using MusicStore.Adapters.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Adapters
{
    class GenericServiceAdapter<TContract, TEntity> : CommonBase.Client.IDataAccess<TContract>
        where TContract : Contracts.IIdentifiable
        where TEntity : TContract, Contracts.ICopyable<TContract>, new()
    {
        private static string Separator => ";";

        public string BaseUri
        {
            get;
        }
        public virtual string ExtUri
        {
            get;
        }

        public GenericServiceAdapter(string baseUri, string extUri)
        {
            BaseUri = baseUri;
            ExtUri = extUri;
        }


        #region Helpers
        protected static string MediaType => "application/json";

        protected HttpClient CreateClient(string baseAddress)
        {
            HttpClient client = new HttpClient();

            if (baseAddress.EndsWith(@"/") == false
                || baseAddress.EndsWith(@"\") == false)
            {
                baseAddress = baseAddress + "/";
            }

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaType));

            return client;
        }
        protected HttpClient GetClient(string baseAddress)
        {
            return CreateClient(baseAddress);
        }


        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TContract> GetAll()
        {
            using (var client = GetClient(BaseUri))
            {
                var task = Task.Run(async () => await client.GetAsync(ExtUri));
                HttpResponseMessage response = task.Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
                    string stringData = task2.Result;
                    return JsonConvert.DeserializeObject<TEntity[]>(stringData) as IEnumerable<TContract>;
                }
                else
                {
                    var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
                    string errorMessage = $"{response.ReasonPhrase}: {task2.Result}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public TContract GetById(int id)
        {
            using (var client = GetClient(BaseUri))
            {
                var task = Task.Run(async () => await client.GetAsync(ExtUri));
                HttpResponseMessage response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
                    string stringData = task2.Result;
                    return (TContract)JsonConvert.DeserializeObject<TEntity>(stringData);
                }
                else
                {
                    var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
                    string errorMessage = $"{response.ReasonPhrase}: {task2.Result}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public TContract Create()
        {
            return new TEntity();
        }

        public TContract Insert(TContract entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TContract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            return Task.Run(() => Count());
        }

        public Task<IEnumerable<TContract>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<TContract> GetByIdAsync(int id)
        {
            return Task.Run(() => GetById(id));
        }

        public Task<TContract> CreateAsync()
        {
            return Task.Run(() => Create());
        }

        public Task<TContract> InsertAsync(TContract entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task UpdateAsync(TContract entity)
        {
            return Task.Run(() => Update(entity));
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(()=> Delete(id));
        }

        public void Dispose()
        {
        }
        #endregion Helpers
    }
}
