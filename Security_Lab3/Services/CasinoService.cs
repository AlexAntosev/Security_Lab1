using System;
using System.Net.Http;
using System.Threading.Tasks;
using Security_Lab3.Constants;
using Security_Lab3.Models;

namespace Security_Lab3.Services
{
    public class CasinoService
    {
        private readonly Http.Http _http;

        public CasinoService()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(CasinoUrls.Base);
            _http = new Http.Http(httpClient);
        }
        
        public async Task<Account> CreateAccount(string id)
        {
            var account = await _http.Get<Account>(CasinoUrls.CreateAccount, ("id", id));
            return account; 
        }
        
        public async Task<PlayResult> Play(string id, long number)
        {
            var playResult = await _http.Get<PlayResult>(
                CasinoUrls.PlayLcg,
                ("id", id),
                ("bet", 1),
                ("number", number));
            
            return playResult; 
        }
    }
}