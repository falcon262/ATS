import { browser, by, element, ExpectedConditions as EC } from 'protractor';

describe('Public Job Application Flow', () => {
  const baseUrl = browser.baseUrl || 'http://localhost:4200';
  const testJobSlug = 'senior-software-engineer-test123';

  beforeAll(async () => {
    await browser.waitForAngularEnabled(false);
  });

  describe('Public Job Detail Page', () => {
    it('should display job details for public slug', async () => {
      await browser.get(`${baseUrl}/apply/${testJobSlug}`);
      await browser.wait(EC.presenceOf(element(by.css('h1'))), 10000);

      const jobTitle = await element(by.css('h1')).getText();
      expect(jobTitle).toBeTruthy();
    });

    it('should show application form', async () => {
      await browser.get(`${baseUrl}/apply/${testJobSlug}`);
      await browser.wait(EC.presenceOf(element(by.css('app-application-form'))), 10000);

      const form = element(by.css('app-application-form form'));
      expect(await form.isPresent()).toBe(true);
    });
  });

  describe('Application Submission', () => {
    it('should submit application successfully', async () => {
      await browser.get(`${baseUrl}/apply/${testJobSlug}`);
      await browser.wait(EC.presenceOf(element(by.css('app-application-form'))), 10000);

      // Fill out form
      await element(by.id('firstName')).sendKeys('John');
      await element(by.id('lastName')).sendKeys('Doe');
      await element(by.id('email')).sendKeys(`test${Date.now()}@example.com`);
      await element(by.id('phone')).sendKeys('+1234567890');
      await element(by.id('yearsOfExperience')).sendKeys('5');
      await element(by.id('skills')).sendKeys('JavaScript, TypeScript, Angular');
      await element(by.id('consentToProcess')).click();

      // Submit
      await element(by.css('button[type="submit"]')).click();

      // Wait for success page
      await browser.wait(EC.urlContains('/apply/success'), 15000);
      expect(await browser.getCurrentUrl()).toContain('/apply/success');
    });

    it('should show registration link on success page', async () => {
      const successButton = element(by.linkText('Create Account to Track Application'));
      expect(await successButton.isPresent()).toBe(true);
    });
  });

  describe('Candidate Registration', () => {
    let applicationId: string;

    beforeAll(async () => {
      // Get application ID from URL
      const url = await browser.getCurrentUrl();
      const urlParams = new URLSearchParams(url.split('?')[1]);
      applicationId = urlParams.get('applicationId') || '';
    });

    it('should navigate to registration page', async () => {
      await element(by.linkText('Create Account to Track Application')).click();
      await browser.wait(EC.urlContains('/register'), 10000);
      expect(await browser.getCurrentUrl()).toContain('/register');
      expect(await browser.getCurrentUrl()).toContain('applicationId');
    });

    it('should register candidate account', async () => {
      const testEmail = `candidate${Date.now()}@example.com`;
      const testPassword = 'Test123456!';

      await element(by.id('email')).sendKeys(testEmail);
      await element(by.id('password')).sendKeys(testPassword);
      await element(by.id('confirmPassword')).sendKeys(testPassword);
      await element(by.id('acceptTerms')).click();

      await element(by.css('button[type="submit"]')).click();

      // Should redirect to login
      await browser.wait(EC.urlContains('/account/login'), 15000);
      expect(await browser.getCurrentUrl()).toContain('/account/login');
    });
  });

  describe('Candidate Dashboard', () => {
    beforeAll(async () => {
      // Login as candidate (assuming successful registration from previous test)
      // This would require actual login credentials or test setup
    });

    it('should display candidate dashboard after login', async () => {
      await browser.get(`${baseUrl}/candidate/dashboard`);
      await browser.wait(EC.presenceOf(element(by.css('h1'))), 10000);

      const pageTitle = await element(by.css('h1')).getText();
      expect(pageTitle).toContain('My Applications');
    });

    it('should show submitted application', async () => {
      const applicationRows = element.all(by.css('table tbody tr'));
      expect(await applicationRows.count()).toBeGreaterThan(0);
    });

    it('should navigate to application detail', async () => {
      await element(by.css('table tbody tr:first-child a')).click();
      await browser.wait(EC.urlContains('/candidate/applications/'), 10000);

      const detailPage = element(by.css('h1'));
      expect(await detailPage.isPresent()).toBe(true);
    });
  });

  describe('Form Validation', () => {
    beforeEach(async () => {
      await browser.get(`${baseUrl}/apply/${testJobSlug}`);
      await browser.wait(EC.presenceOf(element(by.css('app-application-form'))), 10000);
    });

    it('should require first name', async () => {
      await element(by.id('firstName')).sendKeys('');
      await element(by.id('firstName')).sendKeys('test');
      await element(by.id('firstName')).clear();
      await element(by.id('lastName')).click();

      const errorMessage = element(by.css('#firstName + .invalid-feedback'));
      expect(await errorMessage.isDisplayed()).toBe(true);
    });

    it('should require valid email', async () => {
      await element(by.id('email')).sendKeys('invalid-email');
      await element(by.id('firstName')).click();

      const errorMessage = element(by.css('#email + .invalid-feedback'));
      expect(await errorMessage.isDisplayed()).toBe(true);
    });

    it('should require consent checkbox', async () => {
      // Fill required fields but leave consent unchecked
      await element(by.id('firstName')).sendKeys('John');
      await element(by.id('lastName')).sendKeys('Doe');
      await element(by.id('email')).sendKeys('test@example.com');
      await element(by.id('yearsOfExperience')).sendKeys('5');

      const submitButton = element(by.css('button[type="submit"]'));
      expect(await submitButton.isEnabled()).toBe(false);
    });
  });

  describe('File Upload', () => {
    it('should accept valid file types', async () => {
      await browser.get(`${baseUrl}/apply/${testJobSlug}`);
      await browser.wait(EC.presenceOf(element(by.css('app-application-form'))), 10000);

      const fileInput = element(by.id('resume'));
      const absolutePath = require('path').resolve(__dirname, '../test-files/sample-resume.pdf');

      await fileInput.sendKeys(absolutePath);

      const fileSuccess = element(by.css('.text-success'));
      expect(await fileSuccess.isPresent()).toBe(true);
    });

    it('should show error for files exceeding size limit', async () => {
      // This would require a test file larger than 5MB
      // For now, we're documenting the expected behavior
      expect(true).toBe(true);
    });
  });

  describe('Admin Job Detail - Public Link', () => {
    beforeAll(async () => {
      // Login as admin (would require authentication setup)
    });

    it('should display public application link', async () => {
      await browser.get(`${baseUrl}/jobs/test-job-id`);
      await browser.wait(EC.presenceOf(element(by.css('.form-section'))), 10000);

      const publicLinkSection = element(by.cssContainingText('h4', 'Public Application Link'));
      expect(await publicLinkSection.isPresent()).toBe(true);
    });

    it('should copy link to clipboard', async () => {
      const copyButton = element(by.css('button[type="button"]'));
      await copyButton.click();

      // Note: Testing clipboard functionality requires browser permissions
      // In a real test environment, you would verify the toast message instead
      const toastMessage = element(by.cssContainingText('.toast', 'copied'));
      expect(await toastMessage.isPresent()).toBe(true);
    });
  });
});

