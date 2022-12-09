using Newtonsoft.Json;
using SARAPROJECT.Models;

namespace SARAPROJECT.Helpers
{
    public class Sessions
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Sessions(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void sessionUsuarioSet(Usuario oUser)
        {
            string usuario = JsonConvert.SerializeObject(oUser);
            if (oUser.Sexo == "M")
            {
                _httpContextAccessor.HttpContext.Session.SetString("avatarUser", "/images/icon/avatar-male2.png");
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.SetString("avatarUser", "/images/icon/avatar-female.png");
            }

            _httpContextAccessor.HttpContext.Session.SetString("Usuario", usuario); 
        }

        public Usuario sessionUsuarioGet()
        {
            var str = (_httpContextAccessor.HttpContext.Session.GetString("Usuario"));
            var objUsuario = JsonConvert.DeserializeObject<Usuario>(str);

            return objUsuario;
        }

        public String UrlAvatar()
        {
            return (_httpContextAccessor.HttpContext.Session.GetString("avatarUser"));
        }

    }
}
