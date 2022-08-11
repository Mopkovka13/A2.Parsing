using Newtonsoft.Json;
using Parsing.CLI.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace Parsing.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var proxy = new WebProxy("127.0.0.1:8888");
            var coockieContainer = new CookieContainer();

            var PostRequest = new PostRequest($"https://www.lesegais.ru/open-area/graphql");

            PostRequest.Accept = "*/*";
            PostRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36";
            PostRequest.ContentType = "application/json";
            PostRequest.Referer = $"https://www.lesegais.ru/open-area/deal";
            PostRequest.Host = "www.lesegais.ru";
            PostRequest.Proxy = proxy;

            PostRequest.Headers.Add("Origin", $"https://www.lesegais.ru");
            PostRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"104\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"104\"");
            PostRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            PostRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            PostRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            PostRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            PostRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            PostRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            PostRequest.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");


            string resultOfRequest = "";

            //Получение количества записей
            PostRequest.Data = "{\"query\":\"query SearchReportWoodDealCount($size: Int!, $number: Int!, " +
                "$filter: Filter, $orders: [Order!]) {\\n  searchReportWoodDeal(filter: $filter, pageable: " +
                "{number: $number, size: $size}, orders: $orders) {\\n    total\\n    number\\n    size\\n    overallBuyerVolume\\n    overallSellerVolume\\n  " +
                "__typename\\n }\\n}\\n\",\"variables\":{\"size\":20,\"number\":0,\"filter\":null},\"operationName\":\"SearchReportWoodDealCount\"}";

            resultOfRequest = PostRequest.Run(coockieContainer);
            int countOfNotes = JsonConvert.DeserializeObject<Wrapper>(resultOfRequest).Data.searchReportWoodDeal.total;


            //Получение самих записей
            PostRequest.Data = "{\"query\":\"query SearchReportWoodDeal($size: Int!, $number: Int!, " +
                                "$filter: Filter, $orders: [Order!]) {\\n  searchReportWoodDeal(filter: $filter, pageable: " +
                                "{number: $number, size: $size}, orders: $orders) {\\n    content {\\n    sellerName\\n " +
                                "    sellerInn\\n    buyerName\\n    buyerInn\\n    woodVolumeBuyer\\n    woodVolumeSeller\\n    dealDate\\n " +
                                "    dealNumber\\n   __typename\\n  }\\n   __typename\\n  }\\n}\\n \",\"variables\":{\"size\":"+ countOfNotes +",\"" +
                                "number\":\"0\",\"filter\":null,\"orders\":null},\"operationName\":\"SearchReportWoodDeal\"}";


            resultOfRequest = PostRequest.Run(coockieContainer);
            List<Note> notes = JsonConvert.DeserializeObject<Wrapper>(resultOfRequest).Data.searchReportWoodDeal.content;
            Console.ReadKey();
        }
    }
}
