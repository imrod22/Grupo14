using System;
using System.Runtime.Caching;

namespace HomeSwitchHome.Services
{
    public static class CacheHomeSwitchHome
    {
        private static MemoryCache homeSwitchCache = MemoryCache.Default;

        public static object GetFromCache(string nombreLista)
        {
            try
            {
                return homeSwitchCache.Get(nombreLista, null);
            }
            catch(Exception)
            {
                return null;
            }            
        }

        public static void SaveToCache(string nombreLista, object listaGuardar)
        {
            var expiracion = DateTimeOffset.UtcNow.AddMinutes(10);
            homeSwitchCache.Add(nombreLista, listaGuardar, expiracion);            
        }

        public static bool ExistOnCache(string nombreLista)
        {
            return homeSwitchCache.Contains(nombreLista);
        }

        public static void RemoveOnCache(string nombreLista)
        {
            homeSwitchCache.Remove(nombreLista, null);
        }
    }
}