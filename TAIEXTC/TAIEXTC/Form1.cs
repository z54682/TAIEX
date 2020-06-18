using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SKCOMLib;

namespace TAIEXTC
{
    public partial class Form1 : Form
    {
        string strStocks, strID, strPassword;
        double dOneValue, dProportion;
        Dictionary<string, string> dStockList;
        Dictionary<string, long> dMarketValue;
        SKCenterLib m_pSKCenterLib;
        SKQuoteLib m_pSKQuoteLib;
        SKReplyLib m_pSKReply;

        public Form1()
        {
            InitializeComponent();
            m_pSKCenterLib = new SKCenterLib();
            m_pSKQuoteLib = new SKQuoteLib();
            m_pSKReply = new SKReplyLib();
            dStockList = new Dictionary<string, string>();
            dMarketValue = new Dictionary<string, long>();

            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnAnnouncement);

            //讀取股票清單
            StreamReader str = new StreamReader(System.Windows.Forms.Application.StartupPath + "\\StockList.txt");
            strStocks = str.ReadLine();
            str.Close();
            string strQtyStocks;

            //讀取股票總股數
            str = new StreamReader(System.Windows.Forms.Application.StartupPath + "\\QryStock.txt");
            strQtyStocks = str.ReadLine();
            str.Close();

            //讀取設定檔
            str = new StreamReader(System.Windows.Forms.Application.StartupPath + "\\Setting.txt");
            str.ReadLine();
            string[] strTemp = str.ReadLine().Split(',');
            strID = strTemp[0];
            strPassword = strTemp[1];
            dOneValue = Convert.ToDouble(str.ReadLine().Split(',')[0]);
            dProportion = Convert.ToDouble(str.ReadLine().Split(',')[0]);
            str.Close();

            string[] strStock = strStocks.Split(',');
            string[] strQtyStock = strQtyStocks.Split(',');
            for(int i = 0;i < strStock.Length;i++)
            {
                dStockList.Add(strStock[i], strQtyStock[i]);
                dMarketValue.Add(strStock[i], 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_pSKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(SKQuoteLib_OnConnection);
            m_pSKQuoteLib.OnNotifyQuote += new _ISKQuoteLibEvents_OnNotifyQuoteEventHandler(SKQuoteLib_OnNotifyQuote);

            int nCode = m_pSKCenterLib.SKCenterLib_Login(strID, strPassword);
            nCode = m_pSKQuoteLib.SKQuoteLib_EnterMonitor();
        }

        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;
        }

        private void SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if(nKind == 3003 && nCode == 0)
            {
                short nPage = 0;
                m_pSKQuoteLib.SKQuoteLib_RequestStocks(ref nPage, strStocks);
            }
        }

        private void SKQuoteLib_OnNotifyQuote(short sMarketNo, short sStockIndex)
        {
            SKSTOCK pSKStock = new SKSTOCK();
            m_pSKQuoteLib.SKQuoteLib_GetStockByIndex(sMarketNo, sStockIndex, ref pSKStock);

            double dClose = pSKStock.nClose / (Math.Pow(10, pSKStock.sDecimal));
            long lQtyStock = Convert.ToInt64(dStockList[pSKStock.bstrStockNo]);
            dMarketValue[pSKStock.bstrStockNo] = Convert.ToInt64(dClose * lQtyStock * 1000);
            UpdateTAIEX();
        }

        private void UpdateTAIEX()
        {
            long lTotalValue = 0;
            foreach (long i in dMarketValue.Values)
            {
                lTotalValue += i;
            }
            labTAIEXTC.Text = (lTotalValue / dOneValue / (dProportion / 100)).ToString();
        }
    }
}
