# Legal Document Generator

## Overview
The Court Document Generator is a .NET Blazor Server application designed to automate the creation and management of court documents. It allows users to generate, save, and print documents based on predefined templates, with content populated from user-provided form data.

## Features
- **Document Generation:** Create court documents based on templates with dynamic data fields.
- **Unique Document IDs:** Each generated document is assigned a unique ID.
- **Document Storage:** Documents are saved as `.docx` files in a designated folder.
- **PDF Conversion:** Documents are saved as PDF files in the `wwwroot/data/documents` directory.
- **Printing:** Documents can be printed directly from the application.
- **Data Persistence:** Generated documents are stored in a JSON file for future retrieval.

## Technologies Used
- **.NET 8.0:** The application targets .NET 8.0 for modern features and performance.
- **Blazor Server:** Provides a rich, interactive web UI framework for building the application.
- **NetOffice.WordApi:** Used for creating, saving, and printing Word documents.
- **System.Text.Json:** Handles JSON serialization and deserialization for data persistence.
- **Bootstrap:** Utilized for styling and creating a responsive UI.

## Installation

1. **Clone the repository:**
   git clone https://github.com/toyosee/legal-document-generator
   cd CourtDocumentGenerator

2. Install dependencies:
Ensure you have .NET 8.0 installed on your system. Then restore the project dependencies by running:

dotnet restore

3. Build the project:

dotnet build

4. Run the application:

dotnet run

5. Access the application:
Open a browser and navigate to https://localhost:5001 (or the appropriate port shown on applicantion console) to use the application.

Usage
Document Generation

    Navigate to the document generation page.
    Fill in the required form fields.
    Select a document template.
    Click on "Generate Document" to create and save the document.

Document Storage and Retrieval

    Generated documents are stored in wwwroot/data/generatedDocuments.json.
    Document files are saved as .docx in the wwwroot/data/documents directory.

Printing Documents

    After generating a document, it is automatically printed using the system's default printer.

Troubleshooting
File Not Found Error

If you encounter a FileNotFoundException, ensure that:

    The necessary files and directories exist.
    The application has permission to write to the wwwroot/data directory.

COMException Errors

    Make sure that the Microsoft Office installation is compatible with the application.
    Verify that the file paths do not contain invalid characters or exceed the maximum path length.

Incompatibility Warnings

    If you receive warnings about package compatibility with .NET 8.0, ensure all packages are up-to-date or consider alternatives if issues persist.

Contributing
Contributions are welcome! Please fork the repository, make your changes, and submit a pull request.

License
This project is licensed under the MIT License

Contact
For any inquiries or issues, please contact tyabolaji@gmail.com.