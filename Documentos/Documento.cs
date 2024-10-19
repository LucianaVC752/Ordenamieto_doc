

using System.Drawing;

namespace Documentos
{
    public class Documento
    {
        public String Apellido1 { get; set; }
        public String Apellido2 { get; set; }
        public String Nombre { get; set; }
        public String TipoDocumento { get; set; }

        public String NombreCompleto
        {
            get
            {
                return
                    Apellido1 + " " +
                    Apellido2 + " " +
                    Nombre;
            }
        }

        public static List<Documento> documentos;

        public static void DesdeArchivo(String nombreArchivo)
        {
            documentos = new List<Documento>();
            StreamReader sr = Archivo.AbrirArchivo(nombreArchivo);

            if (sr != null)
            {
                String linea = sr.ReadLine();
                linea = sr.ReadLine();
                while (linea != null)
                {
                    String[] datos = linea.Split(';');
                    if (datos.Length >= 4)
                    {
                        Documento documento = new Documento();
                        documento.Apellido1 = datos[0];
                        documento.Apellido2 = datos[1];
                        documento.Nombre = datos[2];
                        documento.TipoDocumento = datos[3];
                        documentos.Add(documento);
                    }
                    linea = sr.ReadLine();
                }
            }
        }

        public static void Mostrar(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("Apellido 1", "Apellido 1");
            dgv.Columns.Add("Apellido 2", "Apellido 2");
            dgv.Columns.Add("Nombre", "Nombre");
            dgv.Columns.Add("Documento", "Documento");

            dgv.Rows.Add(documentos.Count);
            int fila = 0;
            foreach (Documento documento in documentos)
            {
                //dgv.Rows.Add();
                dgv.Rows[fila].Cells[0].Value = documento.Apellido1;
                dgv.Rows[fila].Cells[1].Value = documento.Apellido2;
                dgv.Rows[fila].Cells[2].Value = documento.Nombre;
                dgv.Rows[fila].Cells[3].Value = documento.TipoDocumento;
                fila++;
            }
        }

        // Método para intercambiar elementos
        private static void Intercambiar(int origen, int destino)
        {
            Documento temporal = documentos[origen];
            documentos[origen] = documentos[destino];
            documentos[destino] = temporal;
        }

        private static bool EsMayor(Documento d1, Documento d2, int criterio)
        {
            bool resultado;
            if (criterio == 0)
            {
                // Ordenar primero por Nombre Completo y luego por Tipo de Documento
                resultado = (string.Compare(d1.NombreCompleto, d2.NombreCompleto) > 0 ||
                         (string.Equals(d1.NombreCompleto, d2.NombreCompleto) &&
                         string.Compare(d1.TipoDocumento, d2.TipoDocumento) > 0));
            }
            else
            {
                // Ordenar primero por Tipo de Documento y luego por Nombre Completo
                resultado = (string.Compare(d1.TipoDocumento, d2.TipoDocumento) > 0 ||
                       (string.Equals(d1.TipoDocumento, d2.TipoDocumento) &&
                         string.Compare(d1.NombreCompleto, d2.NombreCompleto) > 0));
            }
            //MessageBox.Show($"¿{d1.NombreCompleto} > {d2.NombreCompleto}? {resultado}");
            //MessageBox.Show(resultado.ToString());
            return resultado;
        }

        public static void OrdenarBurbuja(int criterio)
        {
            for (int i = 0; i < Documento.documentos.Count - 1; i++)
            {
                for (int j = i + 1; j < Documento.documentos.Count; j++)
                {
                    if (EsMayor(Documento.documentos[i], Documento.documentos[j], criterio))
                    {
                        Intercambiar(i, j);
                    }
                }
            }
        }




        private static int ObtenerPivote(int inicio, int fin, int criterio)
        {
            int pivote = inicio;

            Documento documentoPivote = documentos[pivote];
            for (int i = inicio + 1; i <= fin; i++)
            {
                if (EsMayor(documentoPivote, documentos[i], criterio))
                {
                    Intercambiar(i, pivote);
                    pivote++;
                }
                else
                {
                    Intercambiar(i, pivote + 1);
                }
            }
            return pivote;
        }



        public static void OrdenarRapido(int inicio, int fin, int criterio)
        {
            if (inicio >= fin)
            {
                return;
            }
            int pivote = ObtenerPivote(inicio, fin, criterio);
            OrdenarRapido(inicio, pivote - 1, criterio);
            OrdenarRapido(pivote + 1, fin, criterio);

        }

        public static void OrdenarInsercion(int criterio)
        {
            for (int i = 1; i < documentos.Count; i++)
            {
                Documento temp = documentos[i];
                int left = 0;
                int right = i;

                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (EsMayor(documentos[mid], temp, criterio))
                        right = mid;
                    else
                        left = mid + 1;
                }

                for (int j = i; j > left; j--)
                {
                    documentos[j] = documentos[j - 1];
                }
                documentos[left] = temp;
            }

        }

        //NO ME DAAAAAAAAAAAAAAA 

        public static void OrdenamientoSeleccion( int criterio)
        {
            int n = documentos.Count; 
            int minimo;
            for (int i = 0; i < n - 1; i++)
            {
                minimo = i; 
                for (int j = i + 1; j < n; j++)
                {
                    
                    if (EsMayor(documentos[j], documentos[minimo], criterio))
                    {
                        minimo = j; 
                    }
                }
                if (minimo != i)
                {
                    Intercambiar(i, minimo);
                }
            }
        }

        //}for (int r = rango - 1; r > 0; r--)
        //    {
        //        int posicionMayor = 0;
        //        //MessageBox.Show($"Ciclo externo: r = {r}");

        //        for (int ubicacion = 1; ubicacion <= r; ubicacion++)
        //        {
        //            //MessageBox.Show($"Comparando documentos[{ubicacion}] con documentos[{posicionMayor}]");
        //            if (EsMayor(documentos[ubicacion], documentos[posicionMayor], criterio))
        //            {
                        
        //                posicionMayor = ubicacion;
        //                break;
        //            }
        //        }

        //            //MessageBox.Show($"Intercambiando documentos[{r}] con documentos[{posicionMayor}]");
        //            Intercambiar(r, posicionMayor);
                
        //    }
//____________________________________________________________________________________________________________________________________-
        //for (int r = rango - 1; r > 0; r--)
        //            {
        //                //MessageBox.Show($"Iteración externa: i = {r}");
        //                int posicionMayor = 0; 
        //                for (int ubicacion = 1; ubicacion <= r; ubicacion++)
        //                {
        //                    //MessageBox.Show($"Comparando documentos en posiciones {ubicacion} y {posicionMayor}");
        //                    if (EsMayor(documentos[ubicacion], documentos[posicionMayor], criterio))
        //                    {
                        
        //                        posicionMayor = ubicacion;
                        
        //                    }
        //                }
        //                Intercambiar(r, posicionMayor);
        //            }
        //MessageBox.Show("Iniciando ordenamiento por selección...");
//_______________________________________________________________________________________________________________________________________--
        //for (int i = 0; i < rango - 1; i++)
        //{
        //    MessageBox.Show($"Iteración externa: i = {i}");
        //    int minIndex = i;

        //    for (int j = i + 1; j < rango; j++)
        //    {
        //        MessageBox.Show($"Comparando índice {i} con {j}");
        //    }

        //    if (true) // Debería entrar siempre
        //    {
        //        MessageBox.Show($"Intercambiando {documentos[i].NombreCompleto} con {documentos[minIndex].NombreCompleto}");
        //        Intercambiar(i, minIndex);
        //    }
        //}
//_________________________________________________________________________________________________________________________________________
        //De este código me basé de lo que estoy haciendo
        //     def ordenamientoPorSeleccion(unaLista):
        //for llenarRanura in range(len(unaLista)-1,0,-1) :
        //    posicionDelMayor=0
        //    for ubicacion in range(1, llenarRanura+1) :
        //        if unaLista[ubicacion]>unaLista[posicionDelMayor]:
        //            posicionDelMayor = ubicacion

        //    temp = unaLista[llenarRanura]
        //    unaLista[llenarRanura] = unaLista[posicionDelMayor]
        //    unaLista[posicionDelMayor] = temp


        public static void OrdenamientoMezcla(int criterio)
        {
            documentos = DividirListas(documentos, criterio);
        }

        private static List<Documento> DividirListas(List<Documento> documentos, int criterio)
        {
            if (documentos.Count <= 1)
            {
                return documentos;
            }

            int mitad = documentos.Count / 2;
            var aux1 =DividirListas(documentos.GetRange(0, mitad), criterio);
            var aux2 = DividirListas(documentos.GetRange(mitad, documentos.Count - mitad), criterio);

            return JuntarListas(aux1, aux2, criterio);
        }

        private static List<Documento> JuntarListas(List<Documento> aux1, List<Documento> aux2, int criterio)
        {
            List<Documento> resultado = new List<Documento>();
            int aux1Index = 0, aux2Index = 0;

            while (aux1Index < aux1.Count && aux2Index < aux2.Count)
            {
                if (!EsMayor(aux1[aux1Index],  aux2[aux2Index], criterio))
                {
                    resultado.Add(aux1[aux1Index]);
                    aux1Index++;
                }
                else
                {
                    resultado.Add(aux2[aux2Index]);
                    aux2Index++;
                }
            }

            while (aux1Index < aux1.Count)
            {
                resultado.Add(aux1[aux1Index]);
                aux1Index++;
            }

            while (aux2Index < aux2.Count)
            {
                resultado.Add(aux2[aux2Index]);
                aux2Index++;
            }

            return resultado;
        }

    }
}
