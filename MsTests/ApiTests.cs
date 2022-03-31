using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using API;
using System.Threading.Tasks;
using System.Collections.Generic;
using Objects;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MsTests
{
    [TestClass]
    public class ApiTests
    {
        private Users usersApi;
        [TestInitialize]
        public void TestInitialize(){
           usersApi = new();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }

        //Again, should be different tests IMO.
        [TestMethod]
        public async Task TestUsers()
        {
            var response = await usersApi.Get();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(12, response?.Data?.total);
            
            //'GetAll' Could be as 'helper' function inside test suite or in another general 'users' object.
            // Did not add to test suite to not create dependency on API's packages
            List<GetUserData> usersList = await usersApi.GetAll(); 
            
            Assert.IsTrue(usersList.Exists(user => user.first_name == "Tracey" && user.last_name == "Ramos"));
        }

        [TestMethod]         
        public async Task TestSingleUser()
        {
            int userId = 2;
            var apiResult = await usersApi.GetSingleUser(userId);
            Assert.AreEqual(HttpStatusCode.OK, apiResult.StatusCode);
            var users = await usersApi.GetAll();
            var expected = users.Find(user => user.id == userId);
            apiResult.Data.data.Should().BeEquivalentTo(expected);
        }

        [TestMethod]         
        public async Task TestCreate()
        {
            string name = "John";
            string job = "Manager";
            var apiResult = await usersApi.Create(name, job);
            Assert.AreEqual(HttpStatusCode.Created, apiResult.StatusCode);
            var jsonObject = JsonConvert.DeserializeObject(apiResult.Content);
            var userObject = JObject.FromObject(jsonObject);
            userObject.Value<string>("name").Should().BeEquivalentTo(name);
            userObject.Value<DateTime>("createdAt").Should().BeSameDateAs(DateTime.Parse(apiResult.Headers.First(x=>x.Name =="Date").Value.ToString()));
            userObject.Value<string>("job").Should().BeEquivalentTo(job);
        }


        //Definitely not an optimal location, but ran out of time.
        public static double GetPriceFromString(string currencyStr)
        {
            var match = Regex.Match(currencyStr, @"([-+]?[0-9]*\.?[0-9]+)");
            double price = Convert.ToSingle(match.Groups[1].Value);

            return double.Parse(price.ToString("#.##"));
        }
    }
}
