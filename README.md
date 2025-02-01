File Integrity Checker with Basic CRUD Functions

Overview:

The File Integrity Checker is a Windows-based desktop application built using C# and SQL Server. The software allows users to perform essential file management operations and ensure the integrity of files by comparing their hash values. It supports CRUD (Create, Read, Update, Delete) operations for file information.

Features:

User Authentication: Secure login and registration system.

File Integrity Verification: Calculate and compare SHA-256 hash values to verify file integrity.

CRUD Operations:

Create: Add new files with their hash values.

Read: Search and display file information.

Update: Modify file details and recalculate hash values.

Delete: Remove file records.

Components:

Form1 (Login Form) - User login interface.

Links to registration (Form7) and file management functions (Form2).

Form2 (Main Menu) - Provides options to add, update, delete, search, and verify files.

Form3 (Add File) - Allows users to select and add new files. Computes and stores the file's SHA-256 hash.

Form4 (Update File) - Enables updating file details and recalculating the hash.

Form5 (Delete File) - Facilitates file record deletion.

Form6 (Verify File Integrity) - Compares the current hash of a file with the stored hash to check for modifications.

Form7 (User Registration) - Allows new users to register by providing email, username, and password.

How to Use:

Prerequisites

.NET Framework installed on your system.

SQL Server (LocalDB) instance set up.

Setup Instructions

Clone or download the project files.

Open the project in Visual Studio.

Ensure the database file path in the connection string matches your system:

SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\path\\to\\Database1.mdf;Integrated Security=True");

Build and run the application.

Using the Application

Login: Enter your credentials or register as a new user.

Add File: Navigate to the Add File form, select a file, and save it with its hash value.

Search File: Enter a file name to retrieve its details.

Update File: Modify file details and recalculate the hash.

Delete File: Remove a file from the database.

Verify Integrity: Compare the current file hash with the stored hash.

Security Considerations:

Passwords are stored in plaintext in the current implementation. Consider using secure hashing techniques for password storage.

Ensure database file paths are secured and not hardcoded.

Known Issues:

Limited error handling for database connectivity.

Access permissions may be required for certain file operations.

Future Enhancements:

Encrypting stored passwords.

Implementing role-based access control.

Improving UI/UX for better user experience.

Add support for additional hash algorithms.

Author:

Developed by Navneet Kumar.
