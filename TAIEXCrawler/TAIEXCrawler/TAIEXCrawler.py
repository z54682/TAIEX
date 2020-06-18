# coding=big5
import requests as rq
from bs4 import BeautifulSoup
import io
import time
import re

def NumberCommaProcess(number):	#數字處理逗號
	p = re.compile(r'\d,\d')
	while 1:
		m = p.search(number)
		if m:
			mm = m.group()
			number = number.replace(mm,mm.replace(',',''))
		else:
			break
	return number

tStart = time.time()	#處理時間計時
fp = io.open("StockList.txt", "wb+")
i = 1
url1 = "https://www.taifex.com.tw/cht/9/futuresQADetail"
nl_response = rq.get(url1)
soup = BeautifulSoup(nl_response.text, "html.parser")
table = soup.find('table', {'class': 'table_c'})
trs = table.find_all('tr')[1:]
rows = list()
for tr in trs:
    rows.append([td.text.replace('\n', '').replace('\xa0', '') for td in tr.find_all('td')])
i = 0
while (i < 100):
	if (i == 99):
		fp.write((rows[i][1]).strip().encode('utf-8'))
	else:
		fp.write((rows[i][1]).strip().encode('utf-8') + ','.encode('utf-8'))
	i = i + 1
fp.close()
fp = io.open("StockList.txt", "r")
stocks = fp.read()
fp.close()
fp = io.open("QryStock.txt", "wb+")
stock = stocks.split(',')
i = 0
while (i < 100):
	url2 = 'https://stock.capital.com.tw/z/zc/zcm/zcm.djhtm?a=' + stock[i]
	nl_response2 = rq.get(url2)
	nl_response2.encoding = 'big5'
	soup2 = BeautifulSoup(nl_response2.text, "html.parser")
	table2 = soup2.find('table', {'class': 't01'})
	trs2 = table2.find_all('tr')[1:]
	rows2 = list()
	for tr in trs2:
		rows2.append([td.text.replace('\n', '').replace('\xa0', '') for td in tr.find_all('td')])
	if (i == 99):
		fp.write(NumberCommaProcess(rows2[4][1]).strip().encode('utf-8'))
	else:
		fp.write(NumberCommaProcess(rows2[4][1]).strip().encode('utf-8') + ','.encode('utf-8'))
	time.sleep(0.5)
	print(str(i + 1) + '%', end="\r", flush=True)
	i = i + 1
fp.close()
tEnd = time.time()#計時結束
print ("It cost %f sec" % (tEnd - tStart))