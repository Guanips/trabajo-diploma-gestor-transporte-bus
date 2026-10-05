namespace BE
{
    public class Idioma
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool EsDefault { get; set; }
        public int TotalEtiquetas { get; set; }
        public int EtiquetasTraducidas { get; set; }

        public int PorcentajeAvance => TotalEtiquetas == 0 ? 100 : EtiquetasTraducidas * 100 / TotalEtiquetas;

        public Idioma(string codigo, string nombre, bool esDefault = false)
        {
            Codigo = codigo;
            Nombre = nombre;
            EsDefault = esDefault;
        }

        // Para que el ComboBox muestre el nombre lindo automáticamente
        public override string ToString() => Nombre;
    }
}
