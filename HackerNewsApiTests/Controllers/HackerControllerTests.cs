using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using HackerNewsApi.Memory;
using HackerNewsApi.Models;
using HackerNewsApi.HackerApi;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System.Threading;
using Moq;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Controllers.Tests
{
    [TestClass()]
    public class HackerControllerTests
    {
        private static HackerController controller;
        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize]
        public static void init(TestContext testCtx)
        {
            controller = new HackerController(new MemoryCacheImplement<Story>(), new HackerApiClient());
            _testContext = testCtx;
        }

        [TestMethod()]
        public async Task GetAsyncTestAsync()
        {
            //Arrange
            int expectedCount = 20;
            int resultCount;

            //Act
            var result = await controller.GetAsync();
            resultCount = result.Value.Count;
            _testContext.WriteLine($"The call returned {resultCount} results");
            //Assert
            Assert.AreEqual(expectedCount, resultCount);
        }

        [TestMethod()]
        public async Task GetAsyncOrderTestAsync()
        {
            //Arrange

            List<Story> list;
            int resultCount;
            int maxScore = 0;
            int currentScore = 0;
            int i = 0;

            //Act
            var result = await controller.GetAsync();
            list = result.Value;
            resultCount = result.Value.Count;
            _testContext.WriteLine($"The call returned {resultCount} results");

            //Assert
            foreach (var item in list)
            {
                if (i == 0)
                {
                    maxScore = item.score;
                    currentScore = item.score;
                    _testContext.WriteLine($"Setting max score = {maxScore}");
                }
                else
                {
                    MyAsserts.IsSmallerOrEqual(currentScore, item.score);
                    _testContext.WriteLine($"Max score = {maxScore} ,PreviousScore={currentScore},Current Score={item.score}");
                }
                currentScore = item.score;
                i += 1;
            }

        }

        [TestMethod()]
        public async Task GetAsyncPropertiesTestAsync()
        {
            //Arrange

            List<Story> resultItems;
            string[] array = { "title", "uri", "postedBy", "time", "score", "commentCount" };
            int totalProperties = array.Length;
            int propertiesCount = 0;
            int ObjNumber = 1;

            //Act
            var result = await controller.GetAsync();
            resultItems = result.Value;

            //Assert
            foreach (var item in resultItems)
            {
                _testContext.WriteLine($"****Item {ObjNumber}******");
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (array.Contains(prop.Name))
                    {
                        var val = prop.GetValue(item, null);
                        _testContext.WriteLine($" {prop.Name} = {val}");
                        //some entries are missing properties 
                        //this test can fail
                        Assert.IsNotNull(val);
                        propertiesCount += 1;
                    }

                }
                Assert.AreEqual(totalProperties, propertiesCount);
                propertiesCount = 0;
                ObjNumber += 1;
            }

        }
    }
    internal static class MyAsserts
    {
        public static void IsSmaller(int current, int actual)
        {
            if (current > actual)
                return;

            throw new AssertFailedException($"Actual number {actual} is bigger than the current {current}");
        }

        public static void IsSmallerOrEqual(int current, int actual)
        {
            if (current >= actual)
                return;

            throw new AssertFailedException($"Actual number {actual} is bigger than the current {current}");
        }



    }
}


