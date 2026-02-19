# TalentFlow ATS Brand Color Guidelines

## Brand Color Palette

### Primary Colors

#### Orange (#FD5108)
- **Hex**: `#FD5108`
- **RGB**: `R253 G81 B8`
- **CMYK**: `C0 M74 Y96 K0` (Pantone 1655C)
- **Usage**: 
  - ✅ Momentum Mark (logo)
  - ✅ Call to Action buttons
  - ✅ Data visualizations (accent)
  - ✅ Icons (UI/UX only)
  - ✅ Pictograms (tangerine-to-orange gradient)
  - ✅ Illustrations
  - ✅ Links and interactive elements

#### White (#FFFFFF)
- **Hex**: `#FFFFFF`
- **RGB**: `R255 G255 B255`
- **CMYK**: `C0 M0 Y0 K0`
- **Usage**:
  - ✅ Backgrounds (primary)
  - ✅ Text (on dark backgrounds)
  - ✅ Data visualizations
  - ✅ Icons (UI/UX only)
  - ✅ Pictograms (UI/UX only)
  - ✅ Illustrations
  - ✅ Card backgrounds

#### Black (#000000)
- **Hex**: `#000000`
- **RGB**: `R0 G0 B0`
- **CMYK**: `C0 M0 Y0 K100`
- **Usage**:
  - ✅ Text (primary)
  - ✅ Data visualizations (primary)
  - ✅ Icons
  - ✅ Pictograms (UI/UX only)
  - ✅ Illustrations
  - ✅ Headings and body copy

---

## Color Application Rules

### 1. Call to Action (CTA) Buttons
- **Primary CTA**: Orange background, white text
- **Hover State**: Darker orange (#d44507)
- **Active State**: Even darker orange (#ab3906)
- **Shadow**: Orange glow for emphasis

```scss
.btn-primary {
  background: #FD5108;
  color: #FFFFFF;
  
  &:hover {
    background: #d44507;
    box-shadow: 0 4px 12px rgba(253, 81, 8, 0.3);
  }
}
```

### 2. Secondary Buttons
- **Background**: Transparent
- **Border**: Black (2px)
- **Text**: Black
- **Hover**: Black background, white text

```scss
.btn-secondary {
  background: transparent;
  border: 2px solid #000000;
  color: #000000;
  
  &:hover {
    background: #000000;
    color: #FFFFFF;
  }
}
```

### 3. Text Hierarchy
- **Primary Text**: Black (#000000)
- **Secondary Text**: Gray-600 (#666666)
- **Muted Text**: Gray-500 (#808080)
- **Disabled Text**: Gray-400 (#999999)
- **Links**: Orange (#FD5108)

### 4. Backgrounds
- **Primary**: White (#FFFFFF)
- **Secondary**: Gray-50 (#f5f5f5)
- **Tertiary**: Gray-100 (#e6e6e6)
- **Cards**: White with subtle border

### 5. Data Visualizations
- **Primary Series**: Black (#000000)
- **Secondary Series**: Orange (#FD5108)
- **Additional Series**: Gray scale (600, 400)
- **Chart Background**: White
- **Grid Lines**: Gray-200 (#cccccc)

### 6. AI Score Colors
- **High Score (80-100%)**: Green (#10b981) - Success
- **Medium Score (60-79%)**: Orange (#FD5108) - Brand color
- **Low Score (0-59%)**: Red (#ef4444) - Danger

### 7. Status Indicators
- **Active/Published**: Green (#10b981)
- **Pending/In Progress**: Orange (#FD5108)
- **Warning/Review**: Amber (#f59e0b)
- **Inactive/Closed**: Gray (#808080)
- **Rejected/Error**: Red (#ef4444)

---

## Gradients

### Primary Gradient (Tangerine to Orange)
```scss
background: linear-gradient(135deg, #ff9966 0%, #FD5108 100%);
```
**Usage**: Pictograms, decorative elements, hero sections

### Subtle Background Gradient
```scss
background: linear-gradient(180deg, #ffffff 0%, #f5f5f5 100%);
```
**Usage**: Page backgrounds, section dividers

---

## Gray Scale (Black-based)

All grays are derived from black for consistency:

| Name | Hex | Usage |
|------|-----|-------|
| Gray-900 | #1a1a1a | Very dark text, headers |
| Gray-800 | #333333 | Dark text |
| Gray-700 | #4d4d4d | Body text alternative |
| Gray-600 | #666666 | Secondary text |
| Gray-500 | #808080 | Muted text |
| Gray-400 | #999999 | Disabled text, placeholders |
| Gray-300 | #b3b3b3 | Borders |
| Gray-200 | #cccccc | Light borders, dividers |
| Gray-100 | #e6e6e6 | Light backgrounds |
| Gray-50 | #f5f5f5 | Very light backgrounds |

---

## Shadows

All shadows use black with appropriate opacity:

```scss
--shadow-xs: 0 1px 2px rgba(0, 0, 0, 0.05);
--shadow-sm: 0 2px 4px rgba(0, 0, 0, 0.08);
--shadow-md: 0 4px 8px rgba(0, 0, 0, 0.1);
--shadow-lg: 0 8px 16px rgba(0, 0, 0, 0.12);
--shadow-xl: 0 12px 24px rgba(0, 0, 0, 0.15);
--shadow-2xl: 0 20px 40px rgba(0, 0, 0, 0.2);

/* Orange shadows for CTAs */
--shadow-orange: 0 4px 12px rgba(253, 81, 8, 0.3);
--shadow-orange-lg: 0 8px 20px rgba(253, 81, 8, 0.4);
```

---

## Accessibility

### Contrast Ratios (WCAG AA)

✅ **Passing Combinations:**
- Black text on White background: 21:1 (AAA)
- Orange (#FD5108) on White: 4.6:1 (AA for large text)
- White text on Orange: 4.6:1 (AA for large text)
- White text on Black: 21:1 (AAA)
- Gray-600 on White: 5.7:1 (AA)

⚠️ **Use with Caution:**
- Orange text on white for body text (use for headings/CTAs only)
- Light grays (400 and lighter) for important text

### Recommendations:
1. Use **Black** for all body text
2. Use **Orange** for CTAs, links, and accents only
3. Ensure minimum 4.5:1 contrast for normal text
4. Ensure minimum 3:1 contrast for large text (18pt+)
5. Test with color blindness simulators

---

## Icon Colors (UI/UX Only)

Icons should follow these rules:
- **Primary Icons**: Black (#000000)
- **Secondary Icons**: Gray-600 (#666666)
- **Accent Icons**: Orange (#FD5108)
- **Inverse Icons**: White (#FFFFFF) on dark backgrounds
- **Status Icons**: 
  - Success: Green (#10b981)
  - Warning: Amber (#f59e0b)
  - Danger: Red (#ef4444)

---

## Do's and Don'ts

### ✅ DO:
- Use orange for CTAs and momentum marks
- Use black for primary text and data visualizations
- Use white for backgrounds and clean spaces
- Maintain consistent spacing and padding
- Use the tangerine-to-orange gradient for pictograms
- Keep UI clean and minimal

### ❌ DON'T:
- Don't use orange as a background for large areas
- Don't use orange for body text
- Don't mix orange with other bright colors
- Don't use gradients on text
- Don't use orange for negative actions (use red)
- Don't create new colors outside the palette

---

## Component Examples

### Card Component
```scss
.card {
  background: #FFFFFF;
  border: 1px solid #cccccc;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.08);
  border-radius: 12px;
}
```

### Button Component
```scss
/* Primary CTA */
.btn-primary {
  background: #FD5108;
  color: #FFFFFF;
  border: none;
  
  &:hover {
    background: #d44507;
    box-shadow: 0 4px 12px rgba(253, 81, 8, 0.3);
  }
}

/* Secondary */
.btn-secondary {
  background: transparent;
  border: 2px solid #000000;
  color: #000000;
  
  &:hover {
    background: #000000;
    color: #FFFFFF;
  }
}
```

### Badge Component
```scss
.badge {
  padding: 0.375rem 0.75rem;
  border-radius: 20px;
  font-size: 0.75rem;
  font-weight: 600;
  
  &.badge-active {
    background: rgba(16, 185, 129, 0.1);
    color: #10b981;
  }
  
  &.badge-pending {
    background: rgba(253, 81, 8, 0.1);
    color: #FD5108;
  }
}
```

---

## Implementation

All brand colors are defined in:
```
angular/src/app/shared/styles/_brand-colors.scss
```

Import this file in your component styles:
```scss
@import 'app/shared/styles/brand-colors';
```

Use CSS variables in your components:
```scss
.my-component {
  background: var(--brand-white);
  color: var(--text-primary);
  border: 1px solid var(--border-light);
  
  .cta-button {
    background: var(--brand-orange);
    color: var(--brand-white);
    
    &:hover {
      background: var(--brand-orange-dark);
      box-shadow: var(--shadow-orange);
    }
  }
}
```

---

## Brand Assets

### Logo Usage
- **Primary**: Orange momentum mark on white background
- **Inverse**: White momentum mark on black/dark background
- **Minimum Size**: 24px height
- **Clear Space**: Minimum 1x logo height on all sides

### Typography
- **Headings**: Bold, Black color
- **Body**: Regular, Black color
- **Links**: Orange color, underline on hover
- **Captions**: Gray-600 color

---

## Questions?

For brand guidelines questions, refer to the official TalentFlow brand manual or contact the design team.

**Last Updated**: November 2025  
**Version**: 1.0





