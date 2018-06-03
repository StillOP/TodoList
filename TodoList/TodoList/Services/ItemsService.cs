using TodoList.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TodoList.Services
{
    class ItemsService
    {
        static int id = 100;
        private static ObservableCollection<Item> itemcollections = new ObservableCollection<Item>();


        static HttpClient client = new HttpClient();
        const string url = "http://formation-roomy.inow.fr/api/todoitems";

        public ObservableCollection<Item> ItemCollections { get; set; }

        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                dynamic data = JsonConvert.DeserializeObject(json);
                return data;
            }
            return null;
        }

        public static async Task<string> SaveItem(Item item)
        {
            id++;
            item.id = id;

            var json = await Task.Run(() => JsonConvert.SerializeObject(item));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, httpContent);
            return await result.Content.ReadAsStringAsync();
        }

        public static async Task<Item> GetItem(int id)
        {
            Item item = new Item();
            string path = url + "/" + id.ToString();
            var response =  await getDataFromService(path).ConfigureAwait(false);

            if (response["id"] != null)
            {
                item.id = response["id"];
                item.text = response["text"];
                item.description = response["description"];
                item.isDone = response["isDone"];

                return item;
            }
            return item;
        }

        public static async Task<string> GetAllItemsTest()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return json;
            }
            return null;
        }

        public static async Task<ObservableCollection<Item>> GetAllItem()
        {
            ObservableCollection<Item> itemlist = new ObservableCollection<Item>();
            var response = await getDataFromService(url).ConfigureAwait(false);

            if (response != null)
            {
                foreach(var jsonitem in response)
                {
                    Item item = new Item();
                    item.id = jsonitem["id"];
                    item.text = jsonitem["text"];
                    item.description = jsonitem["description"];
                    item.isDone = jsonitem["isDone"];

                    itemlist.Add(item);

                }
            }
            return itemlist;
        }

        public static async Task<string> ItemIsDone(Item item)
        {
            HttpClient client = new HttpClient();
            string path = url + "/" + item.id;

            //string jsonString = $"{{'id':{item.id},'text':{item.text},'createdDate':{item.createdDate},'description':{item.description},'isDone':true,'doneDate':{item.doneDate},'priority':{item.priority}}}";
            var json = await Task.Run(() => JsonConvert.SerializeObject(item));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(path, httpContent);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
