using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections;
using System.Data;


namespace Zolech_zadanie1
{
    public partial class Zolech_strona : System.Web.UI.Page
    {


        protected void Zarejestruj(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            Session["identyfikator"] = MZ_Identyfikator.Text;

            Uzytkownik MZ_this = null;
            foreach (Uzytkownik MZ_u in DBManager.getAll())
            {
                if (MZ_u.getID().Equals(MZ_Identyfikator.Text))
                {
                    DBManager.login(MZ_Identyfikator.Text);
                    MZ_this = MZ_u;
                    break;
                }
            }
            if (MZ_this == null) DBManager.register(MZ_Identyfikator.Text);

            MZ_LoginPanel.Visible = false;
            MZ_Content.Visible = true;
            MZ_zalogowany_uzytkownik.Text = MZ_Identyfikator.Text;
            update(sender, e);
        }

        protected void wybierzUzytkownika(object sender, EventArgs e)
        {
            
        }

        protected void usuwanie(object sender, EventArgs e)
        {
            DBManager.forget(MZ_zalogowany_uzytkownik.Text);
            MZ_Content.Visible = false;
            Session["identyfikator"] = null;
            MZ_LoginPanel.Visible = true;
            MZ_Identyfikator.Text = "";
        }


        public Uzytkownik findById(String MZ_UsID)
        {
            ArrayList MZ_LoggedUsers = (ArrayList)Application["zalogowani"];
            foreach (Uzytkownik MZ_User in MZ_LoggedUsers)
            {
                if (MZ_User.getID().Equals(MZ_UsID))
                {
                    return MZ_User;
                }
            }
            return null;
        }

        protected void czyZalogowany(object source, ServerValidateEventArgs args)
        {
            foreach (Uzytkownik MZ_uz in DBManager.getLogged())
                if (MZ_uz.getID().Equals(MZ_Identyfikator.Text))
                    args.IsValid = false;
        }

        protected void walidacja(object sender, EventArgs e)
        {
            Page.Validate();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (Session.IsNewSession) return;
            else
            {
                ArrayList MZ_UsersRegistredIn = (ArrayList)Application["zalogowani"];
                string MZ_UsID = Session["identyfikator"].ToString();
                MZ_UsersRegistredIn.Remove(findById(MZ_UsID));
                MZ_UsersRegistredIn.Remove(MZ_UsID);
            }

        }

        protected void update(object sender, EventArgs e)
        {
            String MZ_wyb = MZ_lista.SelectedValue;
            MZ_lista.Items.Clear();
            bool pusta = true;
            foreach (Uzytkownik MZ_uz in DBManager.getLogged())
            {
                if (MZ_uz.getID().Equals(MZ_zalogowany_uzytkownik.Text))
                    continue;
                MZ_lista.Items.Add(MZ_uz.getID());
                if (MZ_uz.getID().Equals(MZ_wyb)) MZ_lista.SelectedValue = MZ_wyb;
                pusta = false;
            }
            if (pusta) MZ_lista.Items.Add("Brak użytkowników");
            MZ_DaneUzytkownika.DataBind();
            zalogowany.DataBind();
        }

        public void WypiszDane(Uzytkownik MZ_uz, DBManager dbm)
        {
            DataTable MZ_tabela = new DataTable();
            MZ_tabela.Columns.Add("Data uruchomienia");
            MZ_tabela.Columns.Add("Godzina uruchomienia");
            MZ_tabela.Columns.Add("Ile zmian rozmiaru");
            MZ_tabela.Columns.Add("Ile zmian położenia");
            MZ_tabela.Columns.Add("Ilu uzytkownikow");
            MZ_tabela.Columns.Add("Ilu aplikacji");
            MZ_tabela.Rows.Add(MZ_uz.getDate(), MZ_uz.getTime(), MZ_uz.getZmianyRozmiaru(), MZ_uz.getZmianyPolozenia(), dbm.getALL(), dbm.getLogged1());
            DataSet MZ_zestaw = new DataSet();
            MZ_zestaw.Tables.Add(MZ_tabela);
            MZ_DaneUzytkownika.DataSource = MZ_zestaw.Tables[0];
            MZ_DaneUzytkownika.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Sprawdzenie czy jest to pierwsze zadeni odczytu
            if (!IsPostBack)
            {
                ImageButton1.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                ImageButton2.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                ImageButton3.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                ImageButton4.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                ImageButton5.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                ImageButton5.Attributes.Add("onmouseover", "PageMethods.Change_of_Image(); submit();");
                Session["licznik"] = 1;
                Session["liczWielkosc"] = 0;
                Session["pozycja"] = 0;
                update(sender, e);
            }
            else
            {
                int MZpozycjonuj = (int)Session["licznik"];
                if (MZpozycjonuj == 1)
                {
                    ImageButton1.CssClass = RadioButtonList1.SelectedValue;
                    ImageButton1.Visible = true;
                }
                else
                    ImageButton1.Visible = false;
                if (MZpozycjonuj == 2)
                {
                    ImageButton2.CssClass = RadioButtonList1.SelectedValue;
                    ImageButton2.Visible = true;
                }
                else
                    ImageButton2.Visible = false;

                if (MZpozycjonuj == 3)
                {
                    ImageButton3.CssClass = RadioButtonList1.SelectedValue;
                    ImageButton3.Visible = true;
                }
                else
                    ImageButton3.Visible = false;

                if (MZpozycjonuj == 4)
                {
                    ImageButton4.CssClass = RadioButtonList1.SelectedValue;
                    ImageButton4.Visible = true;
                }
                else
                    ImageButton4.Visible = false;

                if (MZpozycjonuj == 5)
                {
                    ImageButton5.CssClass = RadioButtonList1.SelectedValue;
                    ImageButton5.Visible = true;
                }
                else
                    ImageButton5.Visible = false;
                Label1.Text = (Session["pozycja"]).ToString();
                update(sender, e);
            }
        }


        //POZOSTAWIONE DLA TESTOW
        [WebMethod]
        public static void Change_of_Image()
        {
            Random MZ_losuj = new Random();
            int MZPImagePostition;
            int MZPositionNumber;
            MZPImagePostition = (int)HttpContext.Current.Session["licznik"];
            MZPositionNumber = MZ_losuj.Next(1, 6);
            if (MZPImagePostition == MZPositionNumber)
            {
                MZPositionNumber = MZ_losuj.Next(1, 6);
            }
            HttpContext.Current.Session["pozycja"] = (int)HttpContext.Current.Session["pozycja"] + 1;
            HttpContext.Current.Session["licznik"] = MZPositionNumber;


        }

        //POZOSTAWIONE DLA TESTOW
        [WebMethod]
        public void Choice_of_Size(object sender, EventArgs e)
        {
            int MZliczWielkosc = (int)Session["liczWielkosc"];
            MZliczWielkosc++;
            Session["liczWielkosc"] = MZliczWielkosc;
            Label2.Text = (Session["liczWielkosc"]).ToString();
            update(sender, e);
        }


        protected void ZmianaRozmiaru(object sender, EventArgs e)
        {
            ImageButton[] MZ_images = { ImageButton1, ImageButton2, ImageButton3, ImageButton4, ImageButton5 };
            String css = "";
            if (MZ_radio_maly.Selected == true) css = "maly";
            if (MZ_radio_sredni.Selected == true) css = "sredni";
            if (MZ_radio_duzy.Selected == true) css = "duzy";

            foreach (ImageButton im in MZ_images)
            {
                im.CssClass = css;
            }

            DBManager.rozmiar(MZ_zalogowany_uzytkownik.Text);
            MZ_DaneUzytkownika.DataBind();
            zalogowany.DataBind();
        }


    }
}