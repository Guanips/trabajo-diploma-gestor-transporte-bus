namespace BE.cronograma_entities
{
    // Formato del archivo JSON de exportacion/importacion de cronogramas.
    // Es un contrato independiente de las entidades y de la UI: solo datos, sin ids de la base ni textos traducidos,
    // para que el archivo se pueda abrir en cualquier instalacion y en cualquier idioma.

    public record RutaExportada(string Id, string Descripcion, string Sentido, int DistanciaTotalKM, int TiempoEstimadoMin);

    public record ChoferExportado(int Dni, string NombreCompleto);

    public record InternoExportado(string Patente, string Modelo);

    public record SalidaExportada(
        TimeOnly HoraSalidaTeorica,
        TimeOnly HoraLlegadaTeorica,
        ChoferExportado? Chofer,
        InternoExportado? Interno,
        bool EstaSuspendida,
        string? MotivoSuspension);

    public record CronogramaExportado(
        int FormatoVersion,
        DateTime FechaExportacion,
        string Descripcion,
        DateOnly FechaValidez,
        TimeOnly HoraInicio,
        TimeOnly HoraFin,
        int FrecuenciaMinutos,
        int TiempoDescansoMinutos,
        RutaExportada Ruta,
        List<SalidaExportada> Salidas)
    {
        // Subir este numero ante cambios incompatibles del formato y decidir en el importador cuales versiones se aceptan
        public const int VersionActual = 1;
    }
}
