using System;
using System.Collections.Generic;
using System.Linq;
using Servicio_MB29;
using DAL_MB29;

namespace BLL_MB29
{
    public class UsuarioBLL_MB29
    {
        //Singleton
        private static UsuarioBLL_MB29 _instancia;
        public static UsuarioBLL_MB29 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new UsuarioBLL_MB29();
                return _instancia;
            }
        }

        private const int Intentos_MAX = 3;

        private List<UsuarioServicio_MB29> _usuarios;

        private UsuarioDAL_MB29 _repo;

        private UsuarioBLL_MB29()
        {
            _repo = new UsuarioDAL_MB29();
            _usuarios = _repo.CargarUsuarios_MB29();
        }

        public UsuarioServicio_MB29 Login_MB29(string usuario, string contra)
        {
           var user = _usuarios.FirstOrDefault(u => u.IniciarSesion_MB29(usuario, contra));
            if (user == null)
            {
                BitacoraBLL_MB29.instancia.Registrar_MB29(
                    usuario,
                   "Login Fallido",
                   "Seguridad",
                   $"{usuario} intentó iniciar sesión (usuario no encontrado)",
                   criticidad: 2
                );
                return null;
            }
            if (EstaBloqueado_MB29(user))
            {
                BitacoraBLL_MB29.instancia.Registrar_MB29(
                    usuario,
                   "Login Bloqueado",
                   "Seguridad",
                   $"Se intento iniciar sesion con el usuario {usuario} pero está bloqueado.",
                   criticidad: 2
                );
                return null;
            }

            bool credencialOK = user.IniciarSesion_MB29(usuario, contra);

            if(credencialOK)
            {
                if(user.IntentosErrados_MB29 != 0)
                {
                    user.IntentosErrados_MB29 = 0;
                    _repo.ModificarUsuario_MB29(user);
                    Recargar_MB29();
                }

                SessionManager_MB29.Instancia_MB29.IniciarSesion_MB29(user);

                BitacoraBLL_MB29.instancia.Registrar_MB29(
                    SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                    "Login OK",
                    "Seguridad",
                    $"Usuario {user.Usuario_MB29} inició sesión",
                    criticidad: 1
                );
            }
            else
            {
                //Credenciales incorrectas
                user.IntentosErrados_MB29++;
                if (user.IntentosErrados_MB29 >= Intentos_MAX)
                {
                    user.Bloqueado_MB29 = true;
                }

                _repo.ModificarUsuario_MB29(user);
                Recargar_MB29();
            }
            return user;
        }

        public UsuarioServicio_MB29 ObtenerUsuarioPorNombre_MB29(string usuario)
        {
            return _usuarios.FirstOrDefault(u => string.Equals(u.Usuario_MB29, usuario, StringComparison.OrdinalIgnoreCase));
        }

        public bool EstaBloqueado_MB29(UsuarioServicio_MB29 usuario)
        {
            if (usuario == null) return false;
            if (usuario.Bloqueado_MB29) return true;

            int intentos = BitacoraBLL_MB29.instancia.ObtenerIntentosFallidos_MB29(usuario.Usuario_MB29);

            if (intentos >= Intentos_MAX)
            {
                usuario.Bloqueado_MB29 = true;
                _repo.ModificarUsuario_MB29(usuario);
                Recargar_MB29();
                BitacoraBLL_MB29.instancia.Registrar_MB29(
                    usuario.Usuario_MB29,
                    "Login Bloqueado",
                    "Seguridad",
                    $"El usuario {usuario.Usuario_MB29} superó los intentos máximos y fue bloqueado",
                    criticidad: 3
                );
                return true;
            }
            return false;
        }

        public bool EstaDeshabilitado_MB29(UsuarioServicio_MB29 usuario)
        {
             if(usuario != null)
             {
                if (usuario.Estado_MB29 == "Deshabilitado") return true;
                else
                {
                    return false;
                }
             }
            else { return false; }
        }

        public bool EstaBloqueado_MB29(string user)
        {
            var usuario = _usuarios.FirstOrDefault(u => string.Equals(u.Usuario_MB29, user, StringComparison.OrdinalIgnoreCase));
            return EstaBloqueado_MB29(usuario);
        }

        //Autogenerar usuario y contraseña de usuario nuevo
        public static (string usuario, string contra) AutogenerarCredenciales_MB29(string nombre, string apellido, double dni)
        {
            string dnistring = ((long)dni).ToString();

            string usuario = nombre.Length >= 3 ? nombre.Substring(0, 3).ToLower() : nombre.ToLower();
            usuario += dnistring.Length >= 3 ? dnistring.Substring(0, 3) : dnistring;

            string contra = apellido.Length >= 3 ? apellido.Substring(0, 3).ToLower() : apellido.ToLower();
            contra += dnistring.Length >= 3 ? dnistring.Substring(dnistring.Length - 3) : dnistring;

            return (usuario, contra);
        }

        public void Guardar_MB29(UsuarioServicio_MB29 usuario)
        {
            if (usuario.PassHash_MB29.Length != 44)
            {
                usuario.PassHash_MB29 = Encriptador_MB29.EncriptarPassword_MB29(usuario.PassHash_MB29);
            }
            _repo.GuardarUsuario_MB29(usuario);
            usuario.PrimerLogin_MB29 = true;
            _usuarios.Add(usuario);

            //accion critica por esto nivel 5
            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                "Alta Usuario",
                "Usuarios",
                $"Se creó el usuario {usuario.Usuario_MB29}",
                criticidad: 5
            );
        }

        public void Desbloquear_MB29(UsuarioServicio_MB29 usuario)
        {
            if (!EstaBloqueado_MB29(usuario))
            {
                return;
            }

            usuario.Bloqueado_MB29 = false;
            usuario.IntentosErrados_MB29 = 0;

            _repo.DesbloquearUsuario_MB29(usuario);
            Recargar_MB29();

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                 "Desbloquear Usuario",
                  "Usuarios",
              $"Se desbloqueó el usuario {usuario.Usuario_MB29}",
               criticidad: 4
             );

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                    usuario.Usuario_MB29,
                    "Desbloqueo",
                    "Seguridad",
                    $"{usuario.Usuario_MB29}",
                    criticidad: 1
                );
        }

        public void Modificar_MB29(UsuarioServicio_MB29 usuario)
        {
            if(usuario.PassHash_MB29.Length != 44)
            {
                usuario.PassHash_MB29 = Encriptador_MB29.EncriptarPassword_MB29(usuario.PassHash_MB29);
            }            
            _repo.ModificarUsuario_MB29(usuario);
            Recargar_MB29();

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                 "Modificar Usuario",
                  "Usuarios",
              $"Se modificó el usuario {usuario.Usuario_MB29}",
               criticidad: 4
             );
        }

        public void Deshabilitar_MB29(UsuarioServicio_MB29 usuario)
        {
            if (EstaDeshabilitado_MB29(usuario))
            {
                return;
            }

            usuario.Estado_MB29 = "Deshabilitado";

            _repo.DeshabilitarUsuario_MB29(usuario);
            Recargar_MB29();

            //accion critica por esto nivel 5
            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                "Deshabilitar Usuario",
                "Usuarios",
                $"Se deshabilitó el usuario {usuario.Usuario_MB29}",
                criticidad: 5
            );
        }

        public List<UsuarioServicio_MB29> ObtenerUsuarios_MB29()
        {
            return _usuarios;
        }

        public void Recargar_MB29()
        {
            _usuarios = _repo.CargarUsuarios_MB29();
        }

        public void CambiarContraseña_MB29(UsuarioServicio_MB29 usuario)
        {
            _repo.CambiarContraseña_MB29(usuario);

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                usuario.Usuario_MB29,
                "Cambiar Contraseña",
                "Seguridad",
                $"El usuario {usuario.Usuario_MB29} cambió su contraseña",
                criticidad: 1
            );
        }

        public void MarcarPrimerLoginUsado_MB29(UsuarioServicio_MB29 usuario)
        {
            usuario.PrimerLogin_MB29 = false;
            _repo.MarcarPrimerLoginUsado_MB29(usuario);
            Recargar_MB29();
        }

        public void CerrarSesion_MB29(UsuarioServicio_MB29 usuario)
        {
            SessionManager_MB29.Instancia_MB29.CerrarSesion_MB29();

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                usuario.Usuario_MB29,
                "Logout",
                "Seguridad",
                $"Usuario {usuario.Usuario_MB29} cerró sesión",
                criticidad: 1
            );
        }

        public void GuardarIdioma_MB29(UsuarioServicio_MB29 usuario)
        {
            _repo.GuardarIdioma_MB29(usuario);
        }
    }
}
