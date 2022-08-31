using System;
using System.Linq;
using Insurance.Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);

                            var products = new[]
                                               {
                                                   new
                                                   {
                                                        id = 100,
                                                        name = "No Insurable Product 01",
                                                        productTypeId = 1,
                                                        salesPrice = 1000
                                                   },
                                                   new
                                                   {
                                                        id = 200,
                                                        name = "Test Product 02",
                                                        productTypeId = 2,
                                                        salesPrice = 499
                                                   },
                                                   new
                                                   {
                                                        id = 201,
                                                        name = "Test Product 03",
                                                        productTypeId = 2,
                                                        salesPrice = 500
                                                   },
                                                   new
                                                   {
                                                        id = 202,
                                                        name = "Test Product 04",
                                                        productTypeId = 2,
                                                        salesPrice = 2000
                                                   },
                                                   new
                                                   {
                                                        id = 300,
                                                        name = "Test Laptop 01",
                                                        productTypeId = 3,
                                                        salesPrice = 499
                                                   },
                                                   new
                                                   {
                                                        id = 301,
                                                        name = "Test Laptop 02",
                                                        productTypeId = 3,
                                                        salesPrice = 500
                                                   },
                                                   new
                                                   {
                                                        id = 302,
                                                        name = "Test Laptop 03",
                                                        productTypeId = 3,
                                                        salesPrice = 2000
                                                   },
                                                   new
                                                   {
                                                        id = 400,
                                                        name = "Test Smartphones 01",
                                                        productTypeId = 4,
                                                        salesPrice = 499
                                                   },
                                                   new
                                                   {
                                                        id = 401,
                                                        name = "Test Smartphones 02",
                                                        productTypeId = 4,
                                                        salesPrice = 500
                                                   },
                                                   new
                                                   {
                                                        id = 402,
                                                        name = "Test Smartphones 03",
                                                        productTypeId = 4,
                                                        salesPrice = 2000
                                                   }
                                               };

                            var product = products.ToList().First(c => c.id == productId);
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Other types",
                                                       canBeInsured = false
                                                   },
                                                   new
                                                   {
                                                       id = 2,
                                                       name = "Other types",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 3,
                                                       name = "Laptops",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 4,
                                                       name = "Smartphones",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }

    public class InsuranceTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateInsurance_NonInsurableProduct_ShouldNotAddInsuranceCost()
        {
            const float expectedInsuranceValue = 0;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 100,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableProduct_GivenSalesPriceLessThan500Euros_ShouldNotAddInsuranceCost()
        {
            const float expectedInsuranceValue = 0;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 200,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableProduct_GivenSalesPriceBetween500And2000Euros_ShouldAdd1000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 201,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableProduct_GivenSalesPriceGreaterThan2000Euros_ShouldAdd2000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 2000;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 202,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableLaptopProduct_GivenSalesPriceLessThan500Euros_ShouldAdd500toInsuranceCost()
        {
            const float expectedInsuranceValue = 500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 300,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableLaptopProduct_GivenSalesPriceBetween500And2000Euros_ShouldAdd1500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 301,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableLaptopProduct_GivenSalesPriceGreaterThan2000Euros_ShouldAdd2500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 2500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 302,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableSmartphonesProduct_GivenSalesPriceLessThan500Euros_ShouldAdd500toInsuranceCost()
        {
            const float expectedInsuranceValue = 500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 300,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableSmartphonesProduct_GivenSalesPriceBetween500And2000Euros_ShouldAdd1500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 301,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_InsurableSmartphonesProduct_GivenSalesPriceGreaterThan2000Euros_ShouldAdd2500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 2500;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 302,
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
    }

    public class ControllerTestFixture : IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }
}