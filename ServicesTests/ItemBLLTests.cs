using BLL;
using ApiDAL;
using ApiRepos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Responses;
using Moq;
using Models;
using Models.ItemModels;
using Models.DTO;

namespace ServicesTests
{
    [TestClass()]
    public class ItemBLLTests
    {
        [TestMethod()]
        public void AddItemTest()
        {
            Models.ItemModels.Item item = new()
            {
                Name = "Water Cooler Corsair H100 RGB",
                TechnicalDescription = "240mm, intel/Amd, preto - cw - 9060053- WW",
                AcquisitionDate = new DateTime(2024, 02, 17),
                PurchaseValue = 529.99M,
                PurchaseStore = "Kabum",
                ResaleValue = 0,
                Situation = new ItemSituation() { Id = 1 },
                Comment = "Teste comment",
                AcquisitionType = new AcquisitionType() { Id = 1, Name = "Compra" },
                Category = new Category()
                {
                    Id = 1,
                    SubCategory = new SubCategory()
                    {
                        Id = 3
                    }
                },
            };

            ApiResponse? mockResp = new()
            {
                Success = true,
                Content = "{\"Id\":14,\"Name\":\"Water Cooler Corsair H100 RGB\"," +
                "\"TechnicalDescription\":\"240mm, intel/Amd, preto - cw - 9060053- WW\"," +
                "\"AcquisitionDate\":\"2024-02-17\",\"PurchaseValue\":529.99,\"PurchaseStore\":\"Kabum\"," +
                "\"ResaleValue\":0,\"Situation\":{\"Id\":1,\"Name\":\"Em uso\"}," +
                "\"Category\":{\"Id\":1,\"Name\":\"Casa\",\"Color\":\"#bfc9ca\"," +
                "\"SubCategory\":{\"Id\":3,\"Name\":\"Computadores\",\"IconName\":\"Computer\"}}," +
                "\"Comment\":\"Teste de produto comprado \\nPara criaço de teste de unidade\"," +
                "\"Image1\":null,\"Image2\":null," +
                "\"CreatedAt\":\"2024-02-18T13:21:26.0515918+00:00\"," +
                "\"UpdatedAt\":\"2024-02-18T13:21:26.0515951+00:00\"," +
                "\"AcquisitionType\":1,\"WithdrawalDate\":null}"
            };

            string mockJsonItem = "{\"Name\":\"Water Cooler Corsair H100 RGB\",\"TechnicalDescription\":\"240mm, intel/Amd, preto - cw - 9060053- WW\",\"AcquisitionDate\":\"2024-02-17\",\"PurchaseValue\":529.99,\"PurchaseStore\":\"Kabum\",\"ResaleValue\":0,\"SituationId\":1,\"Comment\":\"Teste comment\",\"AcquisitionType\":1,\"Category\":{\"CategoryId\":1,\"SubCategoryId\":3},\"WithdrawalDate\":null}";

            Mock<IHttpClientFunctions> mockHttpClientFunctions = new();
            Mock<IHttpClientWithFileFunctions> mockHttpClientWithFileFunctions = new();
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/item", mockJsonItem)).ReturnsAsync(mockResp);

            ItemApiDAL itemApiDAL = new(mockHttpClientFunctions.Object, mockHttpClientWithFileFunctions.Object);

            ItemBLL itemBLL = new(itemApiDAL);

            var resp = itemBLL.AddItemAsync(item).Result;

            if (resp != null && resp.Success && resp.Content is Models.ItemModels.Item)
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void AltItemAsyncTest()
        {
            Models.ItemModels.Item item = new()
            {
                Id = 1,
                Name = "Water Cooler Corsair H100 RGB",
                TechnicalDescription = "240mm, intel/Amd, preto - cw - 9060053- WW",
                AcquisitionDate = new DateTime(2024, 02, 17),
                PurchaseValue = 529.99M,
                PurchaseStore = "Kabum",
                ResaleValue = 0,
                Situation = new ItemSituation() { Id = 2 },
                Comment = "Teste comment",
                AcquisitionType = new AcquisitionType() { Id = 1 },
                Category = new Category()
                {
                    Id = 1,
                    SubCategory = new SubCategory()
                    {
                        Id = 3
                    }
                },
            };

            //Item itemContent = new()
            //{
            //    Id = 14,
            //    Name = "Water Cooler Corsair H100 RGB",
            //    AcquisitionDate = new DateTime(2024, 02, 17),
            //    AcquisitionType = new AcquisitionType() { Id = 1 },
            //    Category = new Category()
            //    {
            //        Id = 1,
            //        SubCategory = new SubCategory() { Id = 3 }
            //    },
            //    Comment = "Teste de produto comprado \\nPara criaçao de teste de unidade",
            //}

            string mockJsonItem = "{\"Name\":\"Water Cooler Corsair H100 RGB\",\"TechnicalDescription\":\"240mm, intel/Amd, preto - cw " +
                "- 9060053- WW\",\"AcquisitionDate\":\"2024-02-17\",\"PurchaseValue\":529.99,\"PurchaseStore\":\"Kabum\",\"" +
                "ResaleValue\":0,\"SituationId\":2,\"Comment\":\"Teste comment\",\"AcquisitionType\":1,\"Category\":{\"CategoryId\"" +
                ":1,\"SubCategoryId\":3},\"WithdrawalDate\":null}";

            ApiResponse? mockResp = new()
            {
                Success = true,
                Content = "{\"id\":14,\"name\":\"Water Cooler Corsair H100 RGB\",\"technicalDescription\":\"240mm, intel / Amd, preto - cw - 9060053 - WW\"," +
                "\"acquisitionDate\":\"2024 - 02 - 17\",\"purchaseValue\":529.99," +
                "\"purchaseStore\":\"Kabum\",\"resaleValue\":0,\"situation\":{\"id\":2,\"name\":\"Guardado\"}," +
                "\"category\":{\"id\":1,\"name\":\"Casa\",\"color\":\"#bfc9ca\",\"subCategory\":{\"id\":3,\"name\":\"Computadores\",\"iconName\":\"Computer\"}}," +
                "\"comment\":\"Teste de produto comprado \\nPara criaçao de teste de unidade\",\"image1\":null,\"image2\":null," +
                "\"createdAt\":\"2024-02-18T13:21:26.051591\",\"updatedAt\":\"2024-02-22T12:01:30.0804574+00:00\",\"acquisitionType\":1,\"withdrawalDate\":null}"
            };

            Mock<IHttpClientFunctions> mockHttpClientFunctions = new();
            Mock<IHttpClientWithFileFunctions> mockHttpClientWithFileFunctions = new();
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/item/" + 1, mockJsonItem)).ReturnsAsync(mockResp);

            ItemApiDAL itemApiDAL = new(mockHttpClientFunctions.Object, mockHttpClientWithFileFunctions.Object);

            ItemBLL itemBLL = new(itemApiDAL);

            var resp = itemBLL.AltItemAsync(item).Result;

            if (resp != null && resp.Success && resp.Content is null)
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void DelItemAsyncTest()
        {
            ApiResponse? mockResp = new()
            {
                Success = true,
                Content = ""
            };

            Mock<IHttpClientFunctions> mockHttpClientFunctions = new();
            Mock<IHttpClientWithFileFunctions> mockHttpClientWithFileFunctions = new();
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/item/" + 1, null)).ReturnsAsync(mockResp);

            ItemApiDAL itemApiDAL = new(mockHttpClientFunctions.Object, mockHttpClientWithFileFunctions.Object);

            ItemBLL itemBLL = new(itemApiDAL);

            var resp = itemBLL.DelItemAsync(1).Result;

            if (resp != null && resp.Success)
            {
                Assert.IsTrue(true);
                return;

            }

            Assert.Fail();
        }

        [TestMethod()]
        public void GetItemsAsyncTest()
        {

            //atualizar esse json com um mais novo por causa do acquitition type atualizado.

            ApiResponse? mockResp = new()
            {
                Success = true,
                Content = "[{\"id\":5,\"name\":\"Samsung A52\",\"technicalDescription\":\"Teste de descrição técnica\",\"acquisitionDate\":\"2024-02-11\",\"purchaseValue\":1600.00,\"purchaseStore\":\"Amazon\",\"resaleValue\":0.00,\"situation\":{\"id\":1,\"name\":\"Em uso\"},\"category\":{\"id\":2,\"name\":\"Vestimenta\",\"color\":\"#f5cba7\",\"subCategory\":{\"id\":4,\"name\":\"Eletrônicos\",\"iconName\":\"Mobile\"}},\"comment\":\"teste de comentário de teste\\rComentário de testes.\",\"image1\":null,\"image2\":null,\"createdAt\":\"2024-02-11T12:52:04.672912\",\"updatedAt\":\"2024-02-22T12:00:09.411578\",\"acquisitionType\":{\"id\":4,\"name\":\"Eletrônicos\"},\"withdrawalDate\":null},{\"id\":4,\"name\":\"HEADSET GAMER HYPERX CLOUD STINGER 2 CORE PC PRETO\",\"technicalDescription\":\"\",\"acquisitionDate\":\"2023-08-21\",\"purchaseValue\":219.99,\"purchaseStore\":\"Amazon\",\"resaleValue\":1500.00,\"situation\":{\"id\":5,\"name\":\"Revendido\"},\"category\":{\"id\":1,\"name\":\"Casa\",\"color\":\"#bfc9ca\",\"subCategory\":{\"id\":3,\"name\":\"Computadores\",\"iconName\":\"Computer\"}},\"comment\":null,\"image1\":null,\"image2\":null,\"createdAt\":\"2023-12-25T19:39:10.41158\",\"updatedAt\":\"2024-02-22T21:08:23.600593\",\"acquisitionType\":{\"id\":3,\"name\":\"Computadores\"},\"withdrawalDate\":null},{\"id\":1,\"name\":\"Tablet Samsung Galaxy Tab S6 Lite\",\"technicalDescription\":\"64GB, 4GB RAM, Tela Imersiva de 10.4', Câmera Traseira 8MP, Câmera frontal de 5MP, Wifi, Android 13\",\"acquisitionDate\":\"2023-11-11\",\"purchaseValue\":1575.44,\"purchaseStore\":\"Amazon\",\"resaleValue\":null,\"situation\":{\"id\":1,\"name\":\"Em uso\"},\"category\":{\"id\":2,\"name\":\"Vestimenta\",\"color\":\"#f5cba7\",\"subCategory\":{\"id\":4,\"name\":\"Eletrônicos\",\"iconName\":\"Mobile\"}},\"comment\":\"Utilizado para estudos eventuais fora de casa\",\"image1\":null,\"image2\":null,\"createdAt\":\"2023-12-22T17:44:51.022088\",\"updatedAt\":\"2023-12-24T11:48:21.589835\",\"acquisitionType\":{\"id\":4,\"name\":\"Eletrônicos\"},\"withdrawalDate\":null}]"
            };

            ApiResponse? mockTotalsResp = new()
            {
                Success = true,
                Content = "{\"totalItems\":3,\"totalPages\":1}"
            };

            Mock<IHttpClientFunctions> mockHttpClientFunctions = new();
            Mock<IHttpClientWithFileFunctions> mockHttpClientWithFileFunctions = new();
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/totals", null)).ReturnsAsync(mockTotalsResp);
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item?page=" + 1, null)).ReturnsAsync(mockResp);

            ItemApiDAL itemApiDAL = new(mockHttpClientFunctions.Object, mockHttpClientWithFileFunctions.Object);

            ItemBLL itemBLL = new(itemApiDAL);

            var resp = itemBLL.GetItemsAllAsync().Result;

            if (resp != null)
            {
                if (resp is List<Models.ItemModels.Item> items && items.Count == 3)
                {
                    Assert.IsTrue(true);
                    return;
                }
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void GetItemByIdAsyncTest()
        {
            ApiResponse? mockResp = new()
            {
                Success = true,
                Content = "{\"id\":1,\"name\":\"Tablet Samsung Galaxy Tab S6 Lite\",\"technicalDescription\":\"64GB, 4GB RAM, Tela Imersiva de 10.4', Câmera Traseira 8MP, Câmera frontal de 5MP, Wifi, Android 13\",\"acquisitionDate\":\"2023-11-11\",\"purchaseValue\":1575.44,\"purchaseStore\":\"Amazon\",\"resaleValue\":null,\"situation\":{\"id\":1,\"name\":\"Em uso\"},\"category\":{\"id\":2,\"name\":\"Vestimenta\",\"color\":\"#f5cba7\",\"subCategory\":{\"id\":4,\"name\":\"Eletrônicos\",\"iconName\":\"Mobile\"}},\"comment\":\"Utilizado para estudos eventuais fora de casa\",\"image1\":null,\"image2\":null,\"createdAt\":\"2023-12-22T17:44:51.022088\",\"updatedAt\":\"2023-12-24T11:48:21.589835\",\"acquisitionType\":{\"id\":4,\"name\":\"Eletrônicos\"},\"withdrawalDate\":null}"
            };

            Mock<IHttpClientFunctions> mockHttpClientFunctions = new();
            Mock<IHttpClientWithFileFunctions> mockHttpClientWithFileFunctions = new();
            mockHttpClientFunctions.Setup(x => x.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/" + 1, null)).ReturnsAsync(mockResp);

            ItemApiDAL itemApiDAL = new(mockHttpClientFunctions.Object, mockHttpClientWithFileFunctions.Object);

            ItemBLL itemBLL = new(itemApiDAL);

            var resp = itemBLL.GetItemByIdAsync("1").Result;

            if (resp != null && resp.Success && resp.Content is Models.ItemModels.Item)
            {
                if (resp.Content is Models.ItemModels.Item item && item.Id == 1)
                {
                    Assert.IsTrue(true);
                    return;
                }
            }

            Assert.Fail();
        }
    }
}