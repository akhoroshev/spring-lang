#!/usr/bin/python3

import unicodedata
import fileinput

def create_name(s):
    name = ''.join([sym if sym.isalnum() or sym == '_' else unicodedata.name(sym) for sym in s])
    name = name.replace(' ', '_')
    name = name.replace('-', '_')
    return name.upper()

def make_token(s):
    symbol, id = s.rsplit('=', 1)
    symbol = symbol.strip("'")
    symbol_name = create_name(symbol)
    return f'public static readonly SpringTokenType {symbol_name} = new SpringTokenType("{symbol}", {id});'
    
if __name__ == '__main__':
    for line in fileinput.input():
        print(make_token(line.rstrip("\n\r")))
