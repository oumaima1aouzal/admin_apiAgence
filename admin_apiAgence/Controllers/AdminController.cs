using admin_apiAgence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace admin_apiAgence.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AdminController : ControllerBase
    {

        private readonly adminContext _context;
        private readonly AgenceContext _ccontext;
        private readonly UserContext _context1;
        private readonly IJwtAuthenticationServiceManager _JwtAuthenticationServiceManager;
        private readonly IConfiguration _config;

        public AdminController(adminContext context , AgenceContext ccontext, UserContext context1, IJwtAuthenticationServiceManager jwtAuthenticationServiceManager, IConfiguration config) {
            _context = context;
            _ccontext= ccontext;
            _context1= context1;
            _JwtAuthenticationServiceManager = jwtAuthenticationServiceManager;
            _config = config;
        }
        [HttpGet("/agences")]
        public OutputAgence GetAllAgences()
        {
            OutputAgence output = new OutputAgence();
            try
            {
                List<Agence> a =_ccontext.Agence.ToList();

                output.message = "yeep";
                output.list = a;
                if (a.Count == 0)
                {
                    output.Http_Status_Code = "200";
                    output.message = "pas d'agence";
                    output.list = null;
                    return output;
                }
                else
                {
                    output.Http_Status_Code = "200";
                    output.message = " les agences :";
                    output.list = a;
                    return output;
                }
            }

            catch (Exception e)
            {
                output.Http_Status_Code = "404";
                output.message = e.Message;
                output.list = null;
                return output;
            }


        }
        
        [HttpGet("/lister_users")] 
        public OutputUser GetUsers() {


            OutputUser output = new OutputUser();
            try
            {
                List<user> a = _context1.TableUser.ToList();

                output.message = "yeep";
                output.list_utilisateurs = a;
                if (a.Count == 0)
                {
                    output.Http_Status_Code = "200";
                    output.message = "pas d'utilisateurs";
                    output.list_utilisateurs = null;
                    return output;
                }
                else
                {
                    output.Http_Status_Code = "200";
                    output.message = " les utilisateurs :";
                    output.list_utilisateurs = a;
                    return output;
                }
            }

            catch (Exception e)
            {
                output.Http_Status_Code = "404";
                output.message = e.Message;
                output.list_utilisateurs = null;
                return output;
            }



        }
        [HttpGet("/lister_admins")]
        public outputAdmin GetAdmins()
        {


            outputAdmin output = new outputAdmin();
            try
            {
                List<admin> a = _context.TableAdmin.ToList();

                output.message = "yeep";
                output.list_admins = a;
                if (a.Count == 0)
                {
                    output.Http_Status_Code = "200";
                    output.message = "pas d'administrateurs";
                    output.list_admins = null;
                    return output;
                }
                else
                {
                    output.Http_Status_Code = "200";
                    output.message = " les administrateurs :";
                    output.list_admins = a;
                    return output;
                }
            }

            catch (Exception e)
            {
                output.Http_Status_Code = "404";
                output.message = e.Message;
                output.list_admins= null;
                return output;
            }



        }
        //
        [HttpPost("/ajouter_admin")]
        public string AddAdmins([FromBody] admin admin)
        {
           
            try
            {
                var a = _context.TableAdmin.Where(c => c.Email == admin.Email).ToList();
                if(a.Count != 0)
                {
                    var message = "l'admin est  déja  enregistré ";
                    return message;
                }

                _context.Add(admin);
                _context.SaveChanges();
                var admin1 = _JwtAuthenticationServiceManager.Authanticate(admin.Email, admin.password);
                if (admin1 != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,admin.Email),
            };


                    var token = _JwtAuthenticationServiceManager.GenerateToken(_config["Jwt:Key"], claims);
                    var message1 = "l'admin est enregistré ";
                    

                    return message1;
                }

                var message2 = "l'admin n'est pas enregistré";
                
                return message2;

            }

            catch (Exception e)
            {

                
                return e.Message;
            }



        }
        //
        [HttpPut("[action]")]
        public ActionResult EditUser( [FromQuery]int id_user, [FromBody] user user)
        {
            try
            {

                var userToUpdate = _context1.TableUser.Where(u => u.Id == id_user).FirstOrDefault();
                if (userToUpdate == null)
                {
                    return NotFound("user not exist");
                }
                userToUpdate.UserName = user.UserName;
                userToUpdate.Email = user.Email;
                userToUpdate.password = user.password;
                userToUpdate.UserType = user.UserType;
                _context1.SaveChanges();


                return Ok("user has been  edited");

                /*userToUpdate.UserName = user.UserName;
                = user.Email;
                = user.password;
                = user.UserType;
                */
            }
            catch (Exception e)
            {


                return NotFound(e.Message);
            }

        }
        [HttpPut("[action]")]
        public ActionResult EditAgence([FromQuery] int code_agence, [FromBody] Agence agence)
        {
            try
            {
                
                var agenceToUpdate = _ccontext.Agence.Where(u=>u.codeagence== code_agence).FirstOrDefault();

                if (agenceToUpdate == null)
                {
                    return NotFound("agence not exist");
                }
                agenceToUpdate.region= agence.region;
                agenceToUpdate.gps1 = agence.gps1;
                agenceToUpdate.gps2 = agence.gps2;
                agenceToUpdate.region = agence.region;
                agenceToUpdate.codeagence = agence.codeagence;
                agenceToUpdate.adresseagence = agence.adresseagence;
                agenceToUpdate.typesite = agence.typesite;
                agenceToUpdate.gsmsite = agence.gsmsite;
                agenceToUpdate.telsite = agence.telsite;
                agenceToUpdate.horaireouvmatin = agence.horaireouvmatin;
                agenceToUpdate.horairefermmatin = agence.horairefermmatin;
                agenceToUpdate.horaireouvsoir = agence.horaireouvsoir;
                agenceToUpdate.horairefermsoir = agence.horaireouvsoir;
                _ccontext.SaveChanges();


                return Ok("Agence has been  edited");

              

               
            }
            catch (Exception e)
            {


                return NotFound(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context1.TableUser.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context1.TableUser.Remove(user);
            await _context1.SaveChangesAsync();

            return NoContent();
        }





        [HttpGet("{region}")]
       
        public OutputAgence GetAgenceByRegion(String region)
        {
            OutputAgence outpu = new OutputAgence();
            try
            {
                var a = _ccontext.Agence.Where(c => c.region == region).ToList();

                if (a.Count == 0)
                {
                    outpu.Http_Status_Code = "200";
                    outpu.message = "pas d'agence associé a la region saisie";
                    outpu.list = a;
                    return outpu;


                }
                else
                {
                    outpu.Http_Status_Code = "200";
                    outpu.message = "les agences associé à " + region + ":";
                    outpu.list = a;
                    return outpu;
                }
            }
            catch (Exception e)
            {
                outpu.Http_Status_Code = "404";
                outpu.message = e.Message;
                outpu.list = null;
                return outpu;
            }
        }
        [HttpGet("/region")]
        public List<string> GetRegion()
        {

            try
            {
                List<string> region_list = new List<string>();
                List<string> optionList = new List<string> { "pas de regions" };
                var resultat = _ccontext.Agence.ToList();
                if (resultat.Count != 0)
                {
                    for (int i = 0; i < resultat.Count; i++)
                    {
                        region_list.Add(resultat[i].region);
                    }
                    region_list = region_list.Distinct().ToList();
                    return region_list;
                }
                else
                {

                    return optionList;
                }
            }
            catch (Exception e)
            {
                List<string> option = new List<string> { e.Message };
                return option;
            }

        }
        [HttpGet("[action]")]     
        public List<Agence> Get([FromQuery] int gps1, [FromQuery] int gps2, [FromQuery] int R)
        {


            var a = _ccontext.Agence.Where(x => x.gps1 <= gps1 + R
                              && x.gps1 >= gps1 - R
                              && x.gps2 <= gps2 + R
                              && x.gps2 >= gps2 - R).OrderBy(x => Math.Sqrt(Math.Pow((x.gps1 - gps1), 2) + Math.Pow((x.gps2 - gps2), 2))).ToList();
            return a;

        }
    }
}
