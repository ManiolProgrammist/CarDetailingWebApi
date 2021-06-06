using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.HelpModels.PayU;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels
{
   public class PayUHelper
   {
      static string urlAuthorize = "https://secure.snd.payu.com/pl/standard/user/oauth/authorize";
      static string pos_id = "398284";
      static string MD5 = "cbe826cb0fd5fed67850428333fcafa3";
      static string client_id = "398284";
      static string client_secret = "41f0e837936ddee0e3091799925fe45c";
      static string urlOrder = "https://secure.snd.payu.com/api/v2_1/orders";
      //static string urlAuthorize = "https://secure.payu.com/pl/standard/user/oauth/authorize";
      //static string pos_id = "145227";
      //static string MD5 = "13a980d4f851f3d9a1cfc792fb1f5e50";
      //static string client_id = "145227";
      //static string client_secret = "12f071174cb7eb79d4aac5bc2f07563f";
      //static string urlOrder = "https://secure.payu.com/api/v2_1/orders";
      public async Task<T> GetResponsePostString<T>(string url, string json, Dictionary<string, string> headers)
      {
         var httpClient = new HttpClient();
         var buffer = System.Text.Encoding.UTF8.GetBytes(json);
         var byteContent = new ByteArrayContent(buffer);
         HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);
         //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
         T contents = await response.Content.ReadAsAsync<T>();

         return contents;
      }

      public static async Task<Result<PayUOrderRequestResult>> OrderPayU(Order order, string notifyUrl, string customerIp = "127.0.0.1")
      {
         var result = new Result<PayUOrderRequestResult>();
         result.status = false;
         result.info = "";
         var body = "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + client_secret;
         //Needed to setup the body of the request
         StringContent authoriseData = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
         //data.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
         var handler = new HttpClientHandler()
         {
            AllowAutoRedirect = false
         };
         HttpClient client = new HttpClient(handler);
         var response = await client.PostAsync(urlAuthorize, authoriseData);
         var statusCode = response.StatusCode;
         if (statusCode == HttpStatusCode.OK)
         {
            PayUAuthorizeModel authorizeRezult = await response.Content.ReadAsAsync<PayUAuthorizeModel>();
            //
            var orderP = new PayUOrder()
            {
               notifyUrl = notifyUrl,
               continueUrl = "http://localhost:4200/home",
               customerIp = "127.0.0.1",
               merchantPosId = pos_id,
               description = "rejestracja usługi",
               currencyCode = "PLN",
            };
            List<PayuProduct> payuProducts = new List<PayuProduct>();
            PayuProduct pr = new PayuProduct() { name = order.OrdersTemplate.Name, quantity = "1", unitPrice = ((int)(order.OrdersTemplate.MinCost * 100)).ToString() };
            payuProducts.Add(pr);
            orderP.products = payuProducts;
            //orderP.ex = "123123123"; /*order.OrderId.ToString()*/;
            orderP.totalAmount = pr.unitPrice;
            orderP.buyer = new BuyerPayU() { email = "testEmail@gmail.com", firstName = "FirstName", lastName = "lastName", language = "pl", phone = "123123123" };




            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(orderP);
            StringContent orderData = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); //"
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizeRezult.access_token);
            var orderResult = await client.PostAsync(urlOrder, orderData);
            var statusCodeOrder = orderResult.StatusCode;
            if (HttpStatusCode.Found == statusCodeOrder)
            {
               var rV = await orderResult.Content.ReadAsAsync<PayUOrderRequestResult>();
               result.status = true;
               result.info = rV.redirectUri;
               result.value = rV;
            }
            else
            {
               result.info = await orderResult.Content.ReadAsStringAsync();
            }
         }
         else
         {
            result.info = await response.Content.ReadAsStringAsync();
         }
         //It would be better to make sure this request actually made it through



         //close out the client
         client.Dispose();

         return result;
      }

      public async Task<string> Example()
      {
         var result = "";

         var body = "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + client_secret;
         //Needed to setup the body of the request
         StringContent authoriseData = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
         //data.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

         var handler = new HttpClientHandler()
         {
            AllowAutoRedirect = false
         };

         HttpClient client = new HttpClient(handler);

         //Pass in the full URL and the json string content
         var response = await client.PostAsync(urlAuthorize, authoriseData);
         var statusCode = response.StatusCode;
         if (statusCode == HttpStatusCode.OK)
         {
            PayUAuthorizeModel authorizeRezult = await response.Content.ReadAsAsync<PayUAuthorizeModel>();
            //The data that needs to be sent. Any object works.
            var pocoObject = new
            {
               continueUrl = "http://localhost:4200/home",
               customerIp = "127.0.0.1",
               merchantPosId = pos_id,
               description = "RTV market",
               currencyCode = "PLN",
               totalAmount = "300000",
               buyer = new
               {
                  email = "john.doe@example.com",
                  phone = "123123123",
                  firstName = "test",
                  lastName = "test",
                  language = "pl"
               },
               products = new[]
               {
                  new{name="test a",unitPrice="150000",quantity="1" },
                  new{name="testb ",unitPrice="150000",quantity="1" }
               }

            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(pocoObject);
            StringContent orderData = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); //"
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizeRezult.access_token);
            var orderResult = await client.PostAsync(urlOrder, orderData);
            result = await orderResult.Content.ReadAsStringAsync();
         }
         //It would be better to make sure this request actually made it through



         //close out the client
         client.Dispose();

         return result;
      }

      public async Task<PayUAuthorizeModel> authorize()
      {
         Dictionary<string, string> headers = new Dictionary<string, string>();
         headers.Add("Content-Type", "application/x-www-form-urlencoded");
         string body = "{grant_type: client_credentials, client_id: " + client_id + ", client_secret: " + client_secret + "}";
         var task = GetResponsePostString<PayUAuthorizeModel>(urlAuthorize, body, headers);
         //task.Wait();
         PayUAuthorizeModel finalResult = task.Result;
         return finalResult;
      }
   }
}


