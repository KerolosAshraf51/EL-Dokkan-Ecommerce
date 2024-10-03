namespace E_Commerce_API_Angular_Project.DTO
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public double Price { get; set; }

        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }




        // Relationships

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        /*        [JsonIgnore]
                public List<Review>? Reviews { get; set; }
        */
        public List<string>? images { get; set; }

        public Boolean IsDeleted { get; set; }
        // public string ImgUrl { get; set; }
    }
}
