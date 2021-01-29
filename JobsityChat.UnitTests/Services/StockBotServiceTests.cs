using JobsityChat.Models;
using JobsityChat.Repository;
using JobsityChat.Services;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;

namespace JobsityChat.UnitTests
{
    public class StockBotServiceTests
    {
        private StockBotService _stockBotService;
        //private IChatMessageRepository _repository;
        private IOptions<AppServiceSettings> _appServiceSettings;

        [SetUp]
        public void Setup()
        {

            var repoMoq = new Mock<IChatMessageRepository>();
            repoMoq.Setup(r => r.Create(It.IsAny<ChatMessage>()))
                .Returns<ChatMessage>((chatMessage) => { chatMessage.Id = 1; return chatMessage; } );

            _appServiceSettings = Options.Create(new AppServiceSettings(){ StooqUri = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv"});
            this._stockBotService = new StockBotService(_appServiceSettings, repoMoq.Object);
        }

        [Test]
        public void CreateStockMessage_Success()
        {
            //Arrange          
            string stockCode = "aapl.us";

            //Act
            var result = this._stockBotService.CreateStockMessage(stockCode);

            //Assert
            Assert.AreEqual(result.CreatedBy, "BOT");
            Assert.True(result.Message.Contains(stockCode.ToUpper()));
        }

        [Test]
        public void CreateStockMessage_Error()
        {
            //Arrange          
            string stockCode = "";

            //Act
            var ex = Assert.Throws<Exception>(() => this._stockBotService.CreateStockMessage(stockCode));

            //Assert
            Assert.That(ex.Message, Is.EqualTo("stock code cannot be empty"));
        }

        [TestCase("sunw")]
        [TestCase("aapl.us")]
        [TestCase("hpq")]
        public void GetStockValue_Success(string stockCode)
        {
            //Arrange          
            string expected = $"{stockCode.ToUpper()} quote is";

            //Act
            var result = this._stockBotService.GetStockValue(stockCode);

            //Assert
            Assert.True(result.Contains(expected));
        }
    }
}