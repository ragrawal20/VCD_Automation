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


#get the status of a running task
def getTaskStatus(url):
	request = urllib2.Request(url,headers={"x-vcloud-authorization" : token,"Accept" : "application/*+xml;version=1.5"});
	handler = urllib2.urlopen(request);
	task= handler.read();
	#print task;
	tasklistxml = minidom.parseString(task);
	taskelements=tasklistxml.getElementsByTagName('Task');
	arrOrg=[];
	name="";
	
	for node in taskelements:
		name=node.getAttribute('status')
		
	return name;


#url="https://vcd-dn5.pd-cloud.com/api/task/7b3bfc2b-712f-418a-b03e-40e13980ae22";
#status=getTaskStatus(url);
#print status;


#delete vms
def DeleteVms(url):
	openerPut = urllib2.build_opener(urllib2.HTTPHandler);
	requestPut = urllib2.Request(url);
	requestPut.add_header('Accept', 'application/*+xml;version=1.5');
	requestPut.add_header('x-vcloud-authorization', token);
	requestPut.get_method = lambda: 'DELETE';
	urlPut = openerPut.open(requestPut);


#get vmlist for the vapp
def getVMListFromVapp(vApp,Org):
	url='http://10.52.121.174:5000/vcd/environments/id/' + Org + '/' + vApp + '/ALL?format=xml';
	request = urllib2.Request(url);
	handler = urllib2.urlopen(request);
	Vapps= handler.read();
	#print task;
	Vapplistxml = minidom.parseString(Vapps);
	arrVms=[];
	vmelements=Vapplistxml.getElementsByTagName('vm');
	for node in vmelements:
		name="";
		href="";
		name=node.getAttribute('name')
		href=node.getAttribute('id')
		arrVms.append([str(name),str(href)]);
		print "name: "+name+" href: "+href;
		
	
	return arrVms;


#update the task status for the vapp
def updateVappTaskStatus(url):
	request = urllib2.Request(url);
	handler = urllib2.urlopen(request);
	jdata = json.loads(handler.read());
	return jdata;

#get data of all vapps with juststarted status
def getVappTaskStatus(url):
	request = urllib2.Request(url);
	handler = urllib2.urlopen(request);
	jdata = json.loads(handler.read());
	return jdata;


url='http://10.52.121.174:5000/vcd/environments/log/select/vappCreationDetails/*?status=juststarted';
jdata=getVappTaskStatus(url);


for test in jdata['select']:
	print str(test[3]);
	if str(test[3])=="juststarted":
		url="https://vcd-dn5.pd-cloud.com/api/task/"+str(test[0]);
		print url;
		#url="https://vcd-dn5.pd-cloud.com/api/task/a32f74de-3357-40bc-8e92-2ada4c4ad207";
		try:
			status=getTaskStatus(url);
			if status=="success":
				arrVms=getVMListFromVapp(str(test[2]),str(test[4]));
				arrayvm=[];
				for arrVm in arrVms:
					print arrVm[0];
					arrayvm.append(arrVm[0]);
					vmlist=str(test[5]);
					print "vm split";
					vms=vmlist.split('&&');
					if vms=="None":
						vms=vmlist;
					for vm in vms:
						print vm;
					if vmlist=="":
						updateurl='http://10.52.121.174:5000/vcd/environments/log/update/vappCreationDetails/status=netready?vappguid='+str(test[1]);
						jdata=updateVappTaskStatus(updateurl);
						sys.exit(1);
					print "name mis matching";
					sa=set(vms);
					sb=set(arrayvm);
					sc=sb-sa;
					for x in xrange(6):
						for s in sc:
							for arrVm in arrVms:
								if str(s) in arrVm[0]:
									print "name of the vm is: "+str(s)+" and href is: "+arrVm[1]
									url='https://vcd-dn5.pd-cloud.com/api/vApp/vm-'+str(arrVm[1]);
									try:
										DeleteVms(url);
										#updateurl='http://10.52.121.174:5000/vcd/environments/log/update/currentStatus/status=success?vAppGuid='+str(test[1]);
										#jdata=updateVappTaskStatus(updateurl);
									except urllib2.HTTPError,e:
										print e.code;
										print e.read();
									

				print status;
				updateurl='http://10.52.121.174:5000/vcd/environments/log/update/vappCreationDetails/status=netready?vappguid='+str(test[1]);
				jdata=updateVappTaskStatus(updateurl);
		
			if status=="error":
				updateurl='http://10.52.121.174:5000/vcd/environments/log/update/vappCreationDetails/status=error?vappguid='+str(test[1]);
				jdata=updateVappTaskStatus(updateurl);
		except urllib2.HTTPError, e:
			print e.code;
			print e.read();




#url='https://vcd-dn5.pd-cloud.com/api/vApp/vm-39e2a780-9280-48cc-b9d8-4fb9a9eb867e';
#DeleteVms(url);
#print status;










	




