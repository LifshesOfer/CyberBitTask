
using System.Collections.Generic;
using Objects;
using RestSharp;
using System.Threading.Tasks;

namespace API
{
    public class Users : ReqresApi
    {
        public Users()
        {
        }

        public async Task<RestResponse<ReqGetResponse<List<GetUserData>>>> Get(IEnumerable<Parameter>? parameters = null)
        {
            RestRequest request = new("users", Method.Get);
            if(parameters != null)
            {
                request.AddOrUpdateParameters(parameters);
            }
            var response = await restClient.ExecuteAsync<ReqGetResponse<List<GetUserData>>>(request);
            return response;
        }

        public async Task<RestResponse> Create(string nameValue, string jobValue)
        {
            RestRequest request = new("users", Method.Post);
            request.AddJsonBody(new { name = nameValue, job = jobValue });
            var response = await restClient.ExecuteAsync(request);
            return response;
        }
        
        public async Task<List<GetUserData>> GetAll()
        {
            var response = await Get();
            List<GetUserData> usersList = new(response.Data.data);
            for(int page=2; page<= response.Data.total_pages; page++)
            {
                Parameter param = Parameter.CreateParameter("page", "2", ParameterType.QueryString);
                response = await Get(new List<Parameter>{param});
                usersList.AddRange(response.Data.data);
            }
            return usersList;
        }

        public async Task<RestResponse<ReqGetResponse<GetUserData>>> GetSingleUser(int userId)
        {
            RestRequest request = new($"users/{userId}", Method.Get);
            var response = await restClient.ExecuteAsync<ReqGetResponse<GetUserData>>(request);
            return response;
        }
    }
}
