#coding: utf-8 
import urllib2, urllib;
from array import *;

import fileinput;
import os;
import random;

import json;
import sys;
import re;
import base64;
import time;
from urlparse import urlparse;
#import xml.dom.minidom;
from xml.dom import minidom;

from xml.etree import ElementTree;
from xml.dom import pulldom;
from xml.etree.ElementTree import XML, fromstring, tostring;
from xml.etree.ElementTree import ElementTree;


def get_token():
	
	url='https://vcd-dn5.pd-cloud.com/api/sessions';
	password_manager = urllib2.HTTPPasswordMgrWithDefaultRealm(); # o_Ô seriously?
	password_manager.add_password(None, url, 'administrator@System', 'nkrocKjxa5');
	
	auth = urllib2.HTTPBasicAuthHandler(password_manager) ;# create an authentication handler
	opener = urllib2.build_opener(auth); # create an opener with the authentication handler
	urllib2.install_opener(opener); # install the opener... 
	
	
	try:
		
		request = urllib2.Request(url,'',headers={"Accept" : "application/*+xml;version=1.5"});
		handler = urllib2.urlopen(request);
	
		token= handler.info().get('x-vcloud-authorization');
		return token;
	except urllib2.HTTPError, e:
				print e.code;
				print e.read();
	

token=get_token();
print token;


def set_ip(url,vapp,netguid,parentnetwork):
	
	#f1 = open('C:\\vapps_intermediate\\networkapinew.txt').read();
	#f1 = open('C:\\vapps_intermediate\\networktemplate.txt').read();
	f1=open('C:\\eclg\\networktemplate.txt').read();
	xml_data=f1.replace('$vappValue', vapp).replace('$parentNetwork', parentnetwork).replace('$netguid', netguid);
	url2=url;
	try:
		openerPut = urllib2.build_opener(urllib2.HTTPHandler);
		requestPut = urllib2.Request(url2, data=xml_data.encode('utf-8'));
		requestPut.add_header('Accept', 'application/*+xml;version=5.1');
		requestPut.add_header('Content-Type', 'application/vnd.vmware.vcloud.networkConfigSection+xml;charset=ISO-8859-1');
		requestPut.add_header('x-vcloud-authorization', token);
		requestPut.get_method = lambda: 'PUT';
		urlPut = openerPut.open(requestPut);
		
		
				#return "test";
	except urllib2.HTTPError, e:
		print e.code;
		print e.read();

#get data of all vapps with juststarted status
def getVappTaskStatus(url):
	request = urllib2.Request(url);
	handler = urllib2.urlopen(request);
	jdata = json.loads(handler.read());
	return jdata;

#update the task status for the vapp
def updateVappTaskStatus(url):
	request = urllib2.Request(url);
	handler = urllib2.urlopen(request);
	jdata = json.loads(handler.read());
	return jdata;


def powerOnVapp(url):
	request = urllib2.Request(url,'',headers={"x-vcloud-authorization" : token,"Accept" : "application/*+xml;version=1.5","Content-Type" : "application/vnd.vmware.vcloud.task+xml;version=1.5"});
	handler = urllib2.urlopen(request);
	return handler.read();
	

url='http://10.52.121.174:5000/vcd/environments/log/select/vappCreationDetails/*?status=netready';
jdata=getVappTaskStatus(url);

for test in jdata['select']:
	print str(test[3]);
	if str(test[3])=="netready":
		vappurl='https://vcd-dn5.pd-cloud.com/api/vApp/vapp-'+str(test[1])+'/networkConfigSection/';
		vapp=str(test[1]);
		parentnetwork=str(test[6]);
		netguid=str(test[7]);
		try:
			results=set_ip(vappurl,vapp,netguid,parentnetwork);
			time.sleep(60);
			powerstatus=powerOnVapp('https://vcd-dn5.pd-cloud.com/api/vApp/vapp-'+str(test[1])+'/power/action/powerOn');
			
			updateurl='http://10.52.121.174:5000/vcd/environments/log/update/vappCreationDetails/status=success?vappguid='+str(test[1]);
			jdata=updateVappTaskStatus(updateurl);
		except urllib2.HTTPError, e:
			print e.code;
			print e.read();
		
		
#powerstatus=powerOnVapp('https://vcd-dn5.pd-cloud.com/api/vApp/vapp-'+str(test[1])+'/power/action/powerOn');
#powerstatus=powerOnVapp('https://vcd-dn5.pd-cloud.com/api/vApp/vapp-0bbd4f96-b086-4e2a-9d07-31887b8a526f'+'/power/action/powerOn');
#print powerstatus;