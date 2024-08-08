using CourtDocumentGenerator.Models;
using NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourtDocumentGenerator.Services
{
    public class GeneratedDocumentService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "generatedDocuments.json");
        private readonly string _documentsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "documents");
        private readonly Random _random = new Random();

        public async Task<string> SaveGeneratedDocumentAsync(FormData formData, string documentContent)
        {
            formData.DocumentContent = documentContent;

            List<FormData> generatedDocuments;

            try
            {
                if (File.Exists(_filePath))
                {
                    var json = await File.ReadAllTextAsync(_filePath);
                    generatedDocuments = JsonSerializer.Deserialize<List<FormData>>(json) ?? new List<FormData>();
                }
                else
                {
                    generatedDocuments = new List<FormData>();
                }

                // Assign a unique ID to the document
                formData.DocumentId = GenerateUniqueId(generatedDocuments);

                // Save the document as .docx and get the URL
                string documentUrl = await PrintDocumentAsync(formData);

                // Add the document URL to the form data
                formData.DocumentUrl = documentUrl;

                generatedDocuments.Add(formData);

                var updatedJson = JsonSerializer.Serialize(generatedDocuments, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, updatedJson);

                return documentUrl; // Return the URL of the saved document
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving generated document: {ex.Message}");
                throw;
            }
        }

        public async Task<List<FormData>> GetGeneratedDocumentsAsync()
        {
            if (!File.Exists(_filePath)) return new List<FormData>();

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<FormData>>(json) ?? new List<FormData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading generated documents: {ex.Message}");
                return new List<FormData>(); // Return an empty list in case of an error
            }
        }

        public async Task<string> PrintDocumentAsync(FormData formData)
        {
            using (var wordApp = new Application { Visible = false })
            {
                try
                {
                    // Ensure the directory exists
                    if (!Directory.Exists(_documentsDirectory))
                    {
                        Directory.CreateDirectory(_documentsDirectory);
                    }

                    var doc = wordApp.Documents.Add();
                    var range = doc.Range();
                    range.Text = formData.DocumentContent;

                    // Construct a valid file path
                    string filePath = Path.Combine(_documentsDirectory, $"{formData.DocumentId}.docx");

                    // Save the document as a Word file
                    doc.SaveAs2(filePath, WdSaveFormat.wdFormatDocumentDefault);

                    // Close the document without saving
                    doc.Close(WdSaveOptions.wdDoNotSaveChanges);

                    // Return the URL of the saved document
                    return $"/data/documents/{formData.DocumentId}.docx";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving document as .docx: {ex.Message}");
                    throw;
                }
                finally
                {
                    // Quit the application
                    wordApp.Quit();
                }
            }
        }

        private string GenerateUniqueId(List<FormData> existingDocuments)
        {
            string newId;
            do
            {
                newId = GenerateRandomString(5) + GenerateRandomNumber(5);
            } while (existingDocuments.Any(doc => doc.DocumentId == newId));

            return newId;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string GenerateRandomNumber(int length)
        {
            const string numbers = "0123456789";
            return new string(Enumerable.Repeat(numbers, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
