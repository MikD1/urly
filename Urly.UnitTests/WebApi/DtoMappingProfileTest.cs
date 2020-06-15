using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urly.Domain;
using Urly.Dto;
using Urly.WebApi;

namespace Urly.UnitTests.WebApi
{
    [TestClass]
    public class DtoMappingProfileTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(DtoMappingProfile)); });
            _mapper = _configuration.CreateMapper();
        }

        [TestMethod]
        public void ConfigurationIsValid()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MapLinkToLinkDtoIsCorrect()
        {
            var link = new Link("https://urly.dev/abc/123");

            var linkDto = _mapper.Map<LinkDto>(link);

            Assert.AreEqual("https://urly.dev/abc/123", linkDto.FullUrl);
            Assert.AreEqual(string.Empty, linkDto.ShortCode);
        }

        private MapperConfiguration _configuration;
        private IMapper _mapper;
    }
}
