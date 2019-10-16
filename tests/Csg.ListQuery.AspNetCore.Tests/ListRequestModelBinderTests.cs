using Microsoft.VisualStudio.TestTools.UnitTesting;
using Csg.ListQuery.AspNetCore;
using System.Linq;
using System;

namespace Csg.ListQuery.AspNetCore.Tests
{
    [TestClass]
    public class ListRequestModelBinderTests
    {
        [TestMethod]
        public void CreateRequest_FromString()
        {
            var queryString = "fields=PersonID,FirstName,LastName,BirthDate&where[firstName]=bob&order=LastName&order=-FirstName";
            var request = ModelBinding.ListRequestQueryStringModelBinder.CreateRequest<ListRequest>(queryString);

            Assert.AreEqual(4, request.Fields.Count());
            Assert.AreEqual("PersonID", request.Fields.First(), true);
            Assert.AreEqual("FirstName", request.Fields.Skip(1).First(), true);
            Assert.AreEqual("LastName", request.Fields.Skip(2).First(), true);
            Assert.AreEqual("BirthDate", request.Fields.Last(), true);

            Assert.AreEqual(1, request.Filters.Count());
            Assert.AreEqual("FirstName", request.Filters.First().Name, true);
            Assert.AreEqual("bob", request.Filters.First().Value);
            Assert.AreEqual(Csg.ListQuery.Abstractions.ListFilterOperator.Equal, request.Filters.First().Operator.Value);

            Assert.AreEqual(2, request.Sort.Count());
            Assert.AreEqual("LastName", request.Sort.First().Name, true);
            Assert.AreEqual("FirstName", request.Sort.Last().Name, true);
            Assert.AreEqual(true, request.Sort.Last().SortDescending);
        }

        [TestMethod]
        public void CreateRequest_UnsupportedPaging_Throws()
        {
            var queryString = "offset=50&limit=10";

            Assert.ThrowsException<NotSupportedException>(() =>
            {
                ModelBinding.ListRequestQueryStringModelBinder.CreateRequest<ListRequest>(queryString);
            });            
        }

        [TestMethod]
        public void CreateRequest_FromStringWithPaging()
        {
            var queryString = "fields=PersonID,FirstName,LastName,BirthDate&where[firstName]=bob&order=LastName&order=-FirstName&offset=50&limit=10";
            var request = ModelBinding.ListRequestQueryStringModelBinder.CreateRequest<PagedListRequest>(queryString);

            Assert.AreEqual(4, request.Fields.Count());
            Assert.AreEqual("PersonID", request.Fields.First(), true);
            Assert.AreEqual("FirstName", request.Fields.Skip(1).First(), true);
            Assert.AreEqual("LastName", request.Fields.Skip(2).First(), true);
            Assert.AreEqual("BirthDate", request.Fields.Last(), true);

            Assert.AreEqual(1, request.Filters.Count());
            Assert.AreEqual("FirstName", request.Filters.First().Name, true);
            Assert.AreEqual("bob", request.Filters.First().Value);
            Assert.AreEqual(Csg.ListQuery.Abstractions.ListFilterOperator.Equal, request.Filters.First().Operator.Value);

            Assert.AreEqual(2, request.Sort.Count());
            Assert.AreEqual("LastName", request.Sort.First().Name, true);
            Assert.AreEqual("FirstName", request.Sort.Last().Name, true);
            Assert.AreEqual(true, request.Sort.Last().SortDescending);

            Assert.AreEqual(10, request.Limit);
            Assert.AreEqual(50, request.Offset);
        }

        //[TestMethod]
        //public void CreateRequest_FromString_Perf()
        //{
        //    var queryString = "fields=PersonID,FirstName,LastName,BirthDate&where[firstName]=bob&order=LastName";
        //    var request = ModelBinding.ListRequestModelBinder.CreateRequest<ListRequest<Person>>(queryString);

        //    var sw = new System.Diagnostics.Stopwatch();

        //    sw.Start();
        //    for(int i = 1; i < 1000000; i++)
        //    {
        //        ModelBinding.ListRequestModelBinder.CreateRequest<ListRequest<Person>>(queryString);
        //    }
        //    sw.Stop();

        //    System.Diagnostics.Debug.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
        //}
    }
}
