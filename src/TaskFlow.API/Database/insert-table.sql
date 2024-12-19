-- Insert Data into Users Table
INSERT INTO Users (Username, PasswordHash, Email)
VALUES
('john_doe', 'hashed_password_123', 'john@example.com'),
('jane_doe', 'hashed_password_456', 'jane@example.com'),
('luiz_arruda', 'hashed_password_1234', 'luizarruda@example.com');

-- Insert Data into Tasks Table
-- Using the automatically generated Ids for Users
INSERT INTO Tasks (Title, Description, Priority, EndDate, Status, UserId)
VALUES
('Task 1', 'Complete project setup', 3, '2024-12-15 10:00:00', 0, 1),  -- UserId 1 (john_doe)
('Task 2', 'Write API documentation', 1, '2024-12-20 12:00:00', 1, 2),  -- UserId 2 (jane_doe)
('Task 3', 'Test authentication system', 3, '2024-12-18 09:00:00', 2, 3),  -- UserId 3 (luiz_arruda)
('Task 4', 'Review code for project', 0, '2024-12-22 14:00:00', 0, 2),  -- UserId 2 (jane_doe)
('Task 5', 'Fix UI issues in front-end', 2, '2024-12-19 16:00:00', 0, 3);  -- UserId 3 (luiz_arruda)

-- Insert Data into TaskHistory Table
INSERT INTO TaskHistory (TaskId, Status)
VALUES
(1, 1), -- Pending
(2, 2), -- InProgress
(3, 3), -- Completed
(4, 1), -- Pending
(5, 2); -- InProgress

-- Insert Data into TaskLabels Table
INSERT INTO TaskLabels (TaskId, Label)
VALUES
(1, 'Urgent'),
(2, 'Documentation'),
(3, 'Testing'),
(4, 'Review'),
(5, 'UI');

-- Insert Data into RefreshTokens Table
INSERT INTO RefreshTokens (UserId, Token, ExpiresAt)
VALUES
(1, 'refresh_token_example_123', '2024-12-25 23:59:59'),  -- UserId 1 (john_doe)
(2, 'refresh_token_example_456', '2024-12-25 23:59:59'),  -- UserId 2 (jane_doe)
(3, 'refresh_token_example_789', '2024-12-25 23:59:59');  -- UserId 3 (luiz_arruda)
