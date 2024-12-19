-- Drop tables if they exist
DROP TABLE IF EXISTS TaskLabels, TaskHistory, RefreshTokens, Tasks, Users;

-- Create Users Table
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Tasks Table
CREATE TABLE Tasks (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Description TEXT,
    Priority INT NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    Status INT NOT NULL,
    UserId INT NOT NULL REFERENCES Users(Id),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create TaskHistory Table
CREATE TABLE TaskHistory (
    Id SERIAL PRIMARY KEY,
    TaskId INT NOT NULL REFERENCES Tasks(Id) ON DELETE CASCADE,
    Status INT NOT NULL,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create TaskLabels Table
CREATE TABLE TaskLabels (
    Id SERIAL PRIMARY KEY,
    TaskId INT NOT NULL REFERENCES Tasks(Id) ON DELETE CASCADE,
    Label VARCHAR(50) NOT NULL
);

-- Create RefreshTokens Table
CREATE TABLE RefreshTokens (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES Users(Id),
    Token TEXT NOT NULL,
    ExpiresAt TIMESTAMP NOT NULL
);
