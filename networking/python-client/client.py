import socket
import sys
import threading
from gpio import controller

SERVER = ("192.168.5.106", 3333)

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
  conn = sock.connect(SERVER)
  send_reciever()
  thread = threading.Thread(target=listen_for_response())
  thread.start()

except socket.error:
  print("Unable to connect to server")

def handle_response(msg):
  msg_conts = msg.split("@")
  if msg_conts[0] == "lights":
      controller(msg_conts[1])

def listen_for_response():
  while connected:
    data_length = sock.recv(16)
    data = str(sock.recv(data_length))
    handle_response(data)

def send_reciever():
  sock.send("reciever")

