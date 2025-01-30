using Models.Responses;

namespace ServicesTests
{
    [TestClass()]
    public class SubCategoryServicesTests
    {
        [TestMethod()]
        public void ApiToLocalSyncTest()
        {
            ApiResponse response = new ApiResponse() { Content = "[{\"id\":1,\"name\":\"Móveis\",\"iconName\":\"Couch\",\"categoryId\":1,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.919445\"},{\"id\":2,\"name\":\"Eletrodomésticos\",\"iconName\":\"Tv\",\"categoryId\":1,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920007\"},{\"id\":3,\"name\":\"Computadores\",\"iconName\":\"Computer\",\"categoryId\":1,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920009\"},{\"id\":4,\"name\":\"Eletrônicos\",\"iconName\":\"Mobile\",\"categoryId\":2,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920009\"},{\"id\":5,\"name\":\"Calçados\",\"iconName\":\"ShoePrints\",\"categoryId\":2,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920009\"},{\"id\":6,\"name\":\"Roupas\",\"iconName\":\"Tshirt\",\"categoryId\":2,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.92001\"},{\"id\":7,\"name\":\"Utensílios\",\"iconName\":\"AirFreshener\",\"categoryId\":3,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.92001\"},{\"id\":8,\"name\":\"Peças internas\",\"iconName\":\"Wrench\",\"categoryId\":3,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920011\"},{\"id\":9,\"name\":\"Peças externas\",\"iconName\":\"Car\",\"categoryId\":3,\"systemDefault\":true,\"updatedAt\":\"2023-11-26T13:06:25.920025\"}]", Success=true };

            throw new NotImplementedException();
        }
    }
}