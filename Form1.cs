using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace WC_SoapGestionProduit2
{
    public partial class FormAjoutProduit : Form
    {
        public FormAjoutProduit()
        {
            InitializeComponent();
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            lblResultat.Text = "";
            lblResultat.Text = AjouterProduit(txtBNom.Text, txtBQte.Text, txtBPrix.Text);
            //MessageBox.Show(AjouterProduit(txtBNom.Text, txtBQte.Text, txtBPrix.Text));
        }

        private string AjouterProduit(string nom, string strQte, string strPrix)
        {
            try {
                ServiceReference2.RegistrerProduitsClient service = new ServiceReference2.RegistrerProduitsClient();
                if (nom.Length > 1) { 
                    if (int.TryParse(strQte, out int qte) && qte > 0)
                    {
                        if (double.TryParse(strPrix, out double prix) && prix > 0)
                        {
                            ServiceReference2.produit produit = new ServiceReference2.produit
                            {
                                nom = nom,
                                prix = prix,
                                qte = qte
                            };
                            return service.insertProduitObjet(produit);
                            //return service.insertProduitPrimitif(nom, qte, prix);
                        }
                        else
                        errorProvider.SetError(txtBPrix, "ERREUR: Saissir un double et plus grande que zero");
                        txtBPrix.Focus();
                        return "Reessayez svp";
                    }
                    else
                    {
                        errorProvider.SetError(txtBQte, "ERREUR: Saisir un entier et plus grande que zero");
                        txtBQte.Focus();
                        return "Reessayez svp";
                    }
                }else
                {
                    errorProvider.SetError(txtBNom, "ERREUR: Le nom doit avoir au moin 2 caraters");
                    txtBNom.Focus();
                    return "Reessayez svp";
                }
            }
            catch (Exception ex)
            {
                return ("Exception: " + ex.Message);
            }
        }
            
    }
}
