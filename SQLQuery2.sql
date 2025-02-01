CREATE TABLE file_info (
    id INT IDENTITY(1,1) PRIMARY KEY,
    file_name NVARCHAR(MAX) NOT NULL,
    file_address NVARCHAR(MAX) NOT NULL,
    file_hash NVARCHAR(MAX) NOT NULL
);
