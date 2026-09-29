using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Gestoridioma_MB29 : ISujetoComponente
    {
        private static Gestoridioma_MB29 _instancia_MB29;
        public static Gestoridioma_MB29 Instancia_MB29
        {
            get
            {
                if (_instancia_MB29 == null)
                    _instancia_MB29 = new Gestoridioma_MB29();
                return _instancia_MB29;
            }
        }

        private Gestoridioma_MB29()
        {
            IdiomaActual_MB29 = "ES";
            CargarJson_MB29(IdiomaActual_MB29);
        }

        public string IdiomaActual_MB29 { get; private set; }

        private Dictionary<string, string> _traducciones_MB29;
        private readonly List<IObserverIdioma_MB29> _observers_MB29 = new List<IObserverIdioma_MB29>();

       
        public void Agregar_MB29(IObserverIdioma_MB29 observer)
        {
            if (!_observers_MB29.Contains(observer))
                _observers_MB29.Add(observer);
        }

        public void Eliminar_MB29(IObserverIdioma_MB29 observer)
        {
            _observers_MB29.Remove(observer);
        }

        public void Notificar_MB29()
        {
            foreach (var obs in new List<IObserverIdioma_MB29>(_observers_MB29))
                obs.actualizar_MB29(IdiomaActual_MB29); 
        }

        public void CambiarIdioma_MB29(string idioma)
        {
            IdiomaActual_MB29 = idioma;
            CargarJson_MB29(idioma);

            var usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            if (usuario != null)
                usuario.UltimoIdioma_MB29 = idioma;

            Notificar_MB29();
        }

        public void AplicarIdiomaUsuario_MB29()
        {
            var usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            IdiomaActual_MB29 = usuario.UltimoIdioma_MB29;
            CargarJson_MB29(IdiomaActual_MB29);
            Notificar_MB29();
        }

        public string Traducir_MB29(string clave)
        {
            if (_traducciones_MB29 != null && _traducciones_MB29.ContainsKey(clave))
                return _traducciones_MB29[clave];
            return clave; // si no encuentra la clave, devuelve la clave misma
        }

       
        private void CargarJson_MB29(string idioma)
        {
            try
            {
                string ruta = Path.Combine("Idiomas", $"{idioma}.json");
                if (File.Exists(ruta))
                {
                    string json = File.ReadAllText(ruta);
                    _traducciones_MB29 = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }
            catch (Exception)
            {
                _traducciones_MB29 = new Dictionary<string, string>();
            }
        }
    }
}

