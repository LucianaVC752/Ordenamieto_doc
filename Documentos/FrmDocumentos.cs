using System.Reflection;

namespace Documentos
{
    public partial class FrmDocumentos : Form
    {
        public FrmDocumentos()
        {
            InitializeComponent();
            String nombreArchivo = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                @"\Datos\Datos.csv";
            Documento.DesdeArchivo(nombreArchivo);
            Documento.Mostrar(dgvDocumentos);
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            if (cmbCriterio.SelectedIndex >= 0)
            {
                Util.IniciarCronometro();
                Documento.OrdenarBurbuja(cmbCriterio.SelectedIndex);
                txtTiempo.Text = Util.GetTextoTiempoCronometro();
                Documento.Mostrar(dgvDocumentos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un criterio de ordenamiento");
            }
        }

        private void btnOrdenarRapido_Click(object sender, EventArgs e)
        {
            if (cmbCriterio.SelectedIndex >= 0)
            {
                Util.IniciarCronometro();
                Documento.OrdenarRapido(0, Documento.documentos.Count - 1, cmbCriterio.SelectedIndex);
                txtTiempo.Text = Util.GetTextoTiempoCronometro();
                Documento.Mostrar(dgvDocumentos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un criterio de ordenamiento");
            }
        }

        private void btnInsertion_Click(object sender, EventArgs e)
        {
            if (cmbCriterio.SelectedIndex >= 0)
            {
                Util.IniciarCronometro();
                Documento.OrdenarInsercion(cmbCriterio.SelectedIndex);
                txtTiempo.Text = Util.GetTextoTiempoCronometro();
                Documento.Mostrar(dgvDocumentos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un criterio de ordenamiento");
            }
        }

        private void btnSeleccion_Click(object sender, EventArgs e)
        {
            if (cmbCriterio.SelectedIndex >= 0)
            {
                Util.IniciarCronometro();
                Documento.OrdenamientoSeleccion(cmbCriterio.SelectedIndex);
                txtTiempo.Text = Util.GetTextoTiempoCronometro();
                Documento.Mostrar(dgvDocumentos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un criterio de ordenamiento");
            }
        }

        private void btnMezcla_Click(object sender, EventArgs e)
        {
            if (cmbCriterio.SelectedIndex >= 0)
            {
                Util.IniciarCronometro();
                Documento.OrdenamientoMezcla(cmbCriterio.SelectedIndex);
                txtTiempo.Text = Util.GetTextoTiempoCronometro();
                Documento.Mostrar(dgvDocumentos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un criterio de ordenamiento");
            }
        }
    }
}
