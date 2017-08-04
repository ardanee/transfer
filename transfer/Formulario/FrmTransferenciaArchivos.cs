using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace transfer
{
    public partial class FrmTransferenciaArchivos : Form
    {
        private const String ruta = @"D:\megasync\Curso de Angular 4 - Desde cero hasta profesional";
        DirectoryInfo directorio;
        FileInfo[] archivos;

        public FrmTransferenciaArchivos()
        {
            InitializeComponent();
        }

        private void FrmTransferenciaArchivos_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            escanearDirectorio();
        }

        private void escanearDirectorio()
        {
            directorio = new DirectoryInfo(ruta);
            archivos = directorio.GetFiles();

            foreach (FileInfo archivo in archivos)
            {
                /* Console.WriteLine("Nombre :{0}", a.Name);
                 Console.WriteLine("Path :{0}", a.FullName);
                 Console.WriteLine("Extensión :{0}", a.Extension);
                 Console.WriteLine("Bytes :{0}", a.Length);
                 Console.WriteLine("-------------------------");
                 */
                lstFiles.Items.Add(archivo.Name);
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            var task = transferirArchivos();
            task.Start();
            await task;
        }

        private Task transferirArchivos()
        {
            return new Task(() =>
            {
                int nArchivos = archivos.Length;
                int step = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(100.00) / Convert.ToDouble(nArchivos)));
                progressBar1.Step = step;
                foreach (FileInfo archivo in archivos)
                {
                    if (progressBar1.Value + step > 100)
                    {
                        progressBar1.Value = 100;
                    }
                    else
                    {
                        progressBar1.Value += step;
                    }
                    MessageBox.Show("Accion");
                    System.Threading.Thread.Sleep(1000);

                }
            });
        }
    }
}
