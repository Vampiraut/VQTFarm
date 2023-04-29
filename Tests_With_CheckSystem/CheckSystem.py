import json
import logging
import socket
import string
import threading


log_format = '%(asctime)s %(levelname)s %(message)s'
logging.basicConfig(format=log_format, datefmt='%H:%M:%S', level=logging.DEBUG)


def handle_client(sock):
    while True:
        flags = sock.recv(4096).decode()
        if not flags:
            break

        flags = flags[1:]
        flags = flags[:-1]
        flags_mas = flags.split(',')

        response = {}
        for flag in flags_mas:
            if flag[-1] != '=':
                response.update({flag: "incorrect flag"})
            elif flag[-2] in string.digits:
                response.update({flag: "correct flag"})
            else:
                response.update({flag: "bad flag"})
        sock.sendall(json.dumps(response).encode())
    sock.close()


def main():
    serv = socket.socket()
    serv.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    serv.bind(('0.0.0.0', 31337))
    serv.listen(5)

    while True:
        sock, addr = serv.accept()
        logging.info('Got a client: {}'.format(addr))
        threading.Thread(target=handle_client, args=(sock,)).start()


if __name__ == '__main__':
    main()