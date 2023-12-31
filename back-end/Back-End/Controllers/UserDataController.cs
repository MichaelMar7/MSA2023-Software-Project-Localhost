﻿using Back_End.Models;
using Back_End.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private HttpClient _client;
        private readonly UserDataService _service;
        private const string COC_API_KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6ImFhY2EyYTMzLTMwMWUtNDg1YS05ZWI0LWQ1ZTAyNWUzYzZkOCIsImlhdCI6MTY4ODI2Mzg0NSwic3ViIjoiZGV2ZWxvcGVyL2YwNDY0M2VlLTkyMTEtYTUyMi00ODkwLTUwYjZkNDc3M2I3MSIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjQ1Ljc5LjIxOC43OSJdLCJ0eXBlIjoiY2xpZW50In1dfQ.D-kutC64vNdxtSOBYcLj0FOk06iSvSEYTZGzjO34nPYL_cC82jGH3FUPrM4e4Jl-57mFu578ucw3-g4--EgSYQ";
        private const string CR_API_KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjY2ZGVlMjMzLTBiYzgtNDA3OC04NjVlLWRkYTVkNTc3N2Q4NyIsImlhdCI6MTY4ODcxODc0NSwic3ViIjoiZGV2ZWxvcGVyL2FjM2UwN2Y1LTg5YjYtZWU2YS0zYjg0LWQ0MDViY2RkZDI5YSIsInNjb3BlcyI6WyJyb3lhbGUiXSwibGltaXRzIjpbeyJ0aWVyIjoiZGV2ZWxvcGVyL3NpbHZlciIsInR5cGUiOiJ0aHJvdHRsaW5nIn0seyJjaWRycyI6WyI0NS43OS4yMTguNzkiXSwidHlwZSI6ImNsaWVudCJ9XX0.SoKBUmxP2y04gj9-01ekEFDwHAG5ghnwKQPlvgpQVgOoOeD9HSEGbEhWG4kUSHJTuhumOhOESSyRyQEuT9IM7w";
        private const string BS_API_KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjQ5YjcxYTY5LWQ5ZjctNDViMC1hY2I2LWI5NWMyZDQzNzNiYyIsImlhdCI6MTY4ODcxODg2NCwic3ViIjoiZGV2ZWxvcGVyL2UzZDFhMjdlLWRiMDgtY2FmZi01M2Y3LWI3M2ExODgzMzM2ZiIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiNDUuNzkuMjE4Ljc5Il0sInR5cGUiOiJjbGllbnQifV19.4sIgl6INRYximpQy1nMjKjZbFdJZeUkmNRLEgb9aryZCxW-YJ18sDi7h4rzz9SAeDYDRGzGByQdN2a9gGQBrUw";

        public UserDataController(UserDataService service, HttpClient client)
        {
            _client = client;
            _service = service;
        }

        [HttpGet("/{id}/GetCocPlayers")]
        public async Task<List<CocPlayerTag>> GetAllUserCocPlayer(string id)
        {
            return await _service.GetAllCocPlayerTag(id);
        }

        [HttpGet("/{id}/GetCrPlayers")]
        public async Task<List<CrPlayerTag>> GetAllUserCrPlayer(string id)
        {
            return await _service.GetAllCrPlayerTag(id);
        }

        [HttpGet("/{id}/GetBsPlayers")]
        public async Task<List<BsPlayerTag>> GetAllUserBsPlayer(string id)
        {
            return await _service.GetAllBsPlayerTag(id);
        }

        [HttpGet("/{id}/GetCocPlayers/{tag}")]
        public async Task<CocPlayer> GetUserCocPlayer(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", COC_API_KEY);

            CocPlayerTag playerTagObj = await _service.GetCocPlayerTag(id, tag);
            if (playerTagObj == null) return null;

            tag = tag.Substring(1, tag.Length - 1);
            string url = $"https://cocproxy.royaleapi.dev/v1/players/%23{tag}";

            HttpResponseMessage response = await _client.GetAsync(url);

            CocPlayer player = null;
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<CocPlayer>();
                return player!;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpGet("/{id}/GetCrPlayer/{tag}")]
        public async Task<CrPlayer> GetUserCrPlayer(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CR_API_KEY);

            CrPlayerTag playerTagObj = await _service.GetCrPlayerTag(id, tag);
            if (playerTagObj == null) return null;

            tag = tag.Substring(1, tag.Length - 1);
            string url = $"https://proxy.royaleapi.dev/v1/players/%23{tag}";

            HttpResponseMessage response = await _client.GetAsync(url);

            CrPlayer player = null;
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<CrPlayer>();
                return player!;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpGet("/{id}/GetBsPlayer/{tag}")]
        public async Task<BsPlayer> GetUserBsPlayer(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BS_API_KEY);

            BsPlayerTag playerTagObj = await _service.GetBsPlayerTag(id, tag);
            if (playerTagObj == null) return null;

            tag = tag.Substring(1, tag.Length - 1);
            string url = $"https://bsproxy.royaleapi.dev/v1/players/%23{tag}";

            HttpResponseMessage response = await _client.GetAsync(url);

            BsPlayer player = null;
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<BsPlayer>();
                return player!;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpPut("/{id}/AddCocPlayerTag/{tag}")]
        public async Task<IActionResult> AddUserCocPlayerTag(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", COC_API_KEY);
            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://cocproxy.royaleapi.dev/v1/players/%23{urltag}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            await _service.AddCocPlayerTag(id, tag);
            return NoContent();
        }

        [HttpPut("/{id}/AddCrPlayerTag/{tag}")]
        public async Task<IActionResult> AddUserCrPlayerTag(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CR_API_KEY);
            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://proxy.royaleapi.dev/v1/players/%23{urltag}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            await _service.AddCrPlayerTag(id, tag);
            return NoContent();
        }

        [HttpPut("/{id}/AddBsPlayerTag/{tag}")]
        public async Task<IActionResult> AddUserBsPlayerTag(string id, string tag)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BS_API_KEY);
            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://bsproxy.royaleapi.dev/v1/players/%23{urltag}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            await _service.AddBsPlayerTag(id, tag);
            return NoContent();
        }

        [HttpPut("/{id}/AddCocPlayerTag2/{tag}")]
        public async Task<IActionResult> AddUserCocPlayerTag2(string id, string tag, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", COC_API_KEY);

            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://cocproxy.royaleapi.dev/v1/players/%23{urltag}";
            string verifyurl = $"https://cocproxy.royaleapi.dev/v1/players/%23{urltag}/verifytoken";

            var body = new RequestBody { token = token };
            var bodystring = JsonSerializer.Serialize(body);
            var bodycontent = new StringContent(bodystring, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.GetAsync(url);
            HttpResponseMessage verifyresponse = await _client.PostAsync(verifyurl, bodycontent);

            ResponseBody respnosebody = null;
            bool verify = false;
            if (verifyresponse.IsSuccessStatusCode)
            {
                respnosebody = await verifyresponse.Content.ReadAsAsync<ResponseBody>();
                verify = respnosebody.status == "ok";
            }
            else
            {
                throw new Exception(verifyresponse.ReasonPhrase);
            }
            
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            if (!verify)
                throw new Exception("Token is invalid");

            await _service.AddCocPlayerTag(id, tag);
            return NoContent();
        }

        [HttpPut("/{id}/AddCrPlayerTag2/{tag}")]
        public async Task<IActionResult> AddUserCrPlayerTag2(string id, string tag, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CR_API_KEY);

            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://proxy.royaleapi.dev/v1/players/%23{urltag}";
            string verifyurl = $"https://proxy.royaleapi.dev/v1/players/%23{urltag}/verifytoken";

            var body = new RequestBody { token = token };
            var bodystring = JsonSerializer.Serialize(body);
            var bodycontent = new StringContent(bodystring, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.GetAsync(url);
            HttpResponseMessage verifyresponse = await _client.PostAsync(verifyurl, bodycontent);

            ResponseBody respnosebody = null;
            bool verify = false;
            if (verifyresponse.IsSuccessStatusCode)
            {
                respnosebody = await verifyresponse.Content.ReadAsAsync<ResponseBody>();
                verify = respnosebody.status == "ok";
            }
            else
            {
                throw new Exception(verifyresponse.ReasonPhrase);
            }

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            if (!verify)
                throw new Exception("Token is invalid");

            await _service.AddCrPlayerTag(id, tag);
            return NoContent();
        }

        [HttpPut("/{id}/AddBsPlayerTag2/{tag}")]
        public async Task<IActionResult> AddUserBsPlayerTag2(string id, string tag, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BS_API_KEY);

            string urltag = tag.Substring(1, tag.Length - 1);
            string url = $"https://cocproxy.royaleapi.dev/v1/players/%23{urltag}";
            string verifyurl = $"https://bsproxy.royaleapi.dev/v1/players/%23{urltag}/verifytoken";

            var body = new RequestBody { token = token };
            var bodystring = JsonSerializer.Serialize(body);
            var bodycontent = new StringContent(bodystring, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.GetAsync(url);
            HttpResponseMessage verifyresponse = await _client.PostAsync(verifyurl, bodycontent);

            ResponseBody respnosebody = null;
            bool verify = false;
            if (verifyresponse.IsSuccessStatusCode)
            {
                respnosebody = await verifyresponse.Content.ReadAsAsync<ResponseBody>();
                verify = respnosebody.status == "ok";
            }
            else
            {
                throw new Exception(verifyresponse.ReasonPhrase);
            }

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            if (!verify)
                throw new Exception("Token is invalid");

            await _service.AddBsPlayerTag(id, tag);
            return NoContent();
        }


        [HttpDelete("/{id}/RemoveCocPlayerTag/{tag}")]
        public async Task<IActionResult> RemoveUserCocPlayerTag(string id, string tag)
        {
            await _service.RemoveCocPlayerTag(id, tag);
            return NoContent();
        }

        [HttpDelete("/{id}/RemoveCrPlayerTag/{tag}")]
        public async Task<IActionResult> RemoveUserCrPlayerTag(string id, string tag)
        {
            await _service.RemoveCrPlayerTag(id, tag);
            return NoContent();
        }

        [HttpDelete("/{id}/RemoveBsPlayerTag/{tag}")]
        public async Task<IActionResult> RemoveUserBsPlayerTag(string id, string tag)
        {
            await _service.RemoveBsPlayerTag(id, tag);
            return NoContent();
        }

        
        
        
    }
}
