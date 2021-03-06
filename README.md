# SatoriCrypto
簡単なファイル暗号化ソフト

## 使用の前に
**このソフトをご利用になる前に、以下の事項にご同意ください。** 同意できない場合は、このソフトを使用しないでください。

- このソフトで暗号化したことによって生じたいかなる問題に関して、製作者は一切の責任を負いません。
- このソフトで用いられている多重暗号化手法に関して、絶対に安全であるという保証はできません。

## 多重暗号化手法
SatoriCryptoで使用可能な多重暗号化手法を掲載しています。

### XOR
パスワードでファイル内容をバイト単位でXORします。

復号化するには、正しいパスワードが必要となります。

この方法は推奨されておらず、バイナリのパターンによっては簡単にパスワードが割られる可能性があります。

### Password XOR Filename to Key
パスワードとファイル名をXORしたものをキーにし、そのキーとファイル内容をバイト単位でXORするものです。

復号化する際には、正しいファイル名に `.sef` をつけたものと正しいパスワードが必要となります。

この方法もXORと同じく推奨されておらず、バイナリのパターンによっては簡単にパスワードが割られる可能性があります。

### AES
IV にファイル名、Key にパスワードを割り当ててAESで暗号化します。

復号化するには、正しいパスワードが必要となります。

### Password XOR AES
AES暗号化したものにPasswordをXORします。

復号化するには、正しいファイル名に `.sef` をつけたものと正しいパスワードが必要となりますが、正しいファイル名でなくとも、復号化自体は可能です。その場合、先頭15バイトは暗号化前と異なる数値になる可能性があります。