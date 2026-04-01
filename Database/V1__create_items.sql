-- V1__create_items.sql
CREATE TABLE Items (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP
);
