using CourtDocumentGenerator.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourtDocumentGenerator.Services
{
    public class DocumentService
    {
        private readonly string _filePath = "wwwroot/data/documentTemplates.json";

        public async Task<List<CategoryTemplate>> GetDocumentTemplatesAsync()
        {
            if (!File.Exists(_filePath))
            {
                // Log or handle file not found
                return new List<CategoryTemplate>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<CategoryTemplate>>(json) ?? new List<CategoryTemplate>();
            }
            catch (JsonException ex)
            {
                // Log or handle JSON parsing error
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<CategoryTemplate>();
            }
            catch (Exception ex)
            {
                // Log or handle general errors
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<CategoryTemplate>();
            }
        }
    }
}
