-- Backfill Candidate Overall AI Scores from their Applications
-- This script updates each candidate's OverallAIScore with the highest AIScore from all their applications

UPDATE c
SET c.OverallAIScore = (
    SELECT MAX(a.AIScore)
    FROM Applications a
    WHERE a.CandidateId = c.Id
        AND a.AIScore IS NOT NULL
)
FROM Candidates c
WHERE EXISTS (
    SELECT 1 
    FROM Applications a 
    WHERE a.CandidateId = c.Id 
        AND a.AIScore IS NOT NULL
);

-- Show updated candidates
SELECT 
    c.Id as CandidateId,
    c.FirstName + ' ' + c.LastName as CandidateName,
    c.Email,
    c.OverallAIScore,
    (SELECT COUNT(*) FROM Applications a WHERE a.CandidateId = c.Id AND a.AIScore IS NOT NULL) as ApplicationsWithScores,
    (SELECT MAX(a.AIScore) FROM Applications a WHERE a.CandidateId = c.Id) as MaxScore
FROM Candidates c
WHERE c.OverallAIScore IS NOT NULL
ORDER BY c.OverallAIScore DESC;

PRINT '';
PRINT '--- Backfill Complete ---';
PRINT 'Candidates updated: ' + CAST(@@ROWCOUNT AS VARCHAR);

