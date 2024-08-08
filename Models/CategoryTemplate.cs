using System.Collections.Generic;

namespace CourtDocumentGenerator.Models
{
    // Define models for Category and SubCategory
    public class CategoryTemplate
    {
        public string Category { get; set; }
        public List<SubCategoryTemplate> SubCategories { get; set; }
    }

    public class SubCategoryTemplate
    {
        public string SubCategory { get; set; }
        public string Template { get; set; }
    }

}
