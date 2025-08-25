this file contain all what is related of file transimission between my system and Galaxus

first as I mention earlier we work with FTP not API.
so we need to know the configration required:
the username and the password will be provided after the begining of the project.
Hostname:
ftp-partner.digitecgalaxus.ch

Protocol:
SFTP

Port:
22

IP Addresses: Warning Use hostname for connection and IPs only for whitelisting
88.198.35.84
85.10.200.14
116.203.25.19

program:
filezila client

there are some important notes to ensure a safe and stable integration:
1. there are max of 5 parellel sessions
2. to avoid data loss we must wait for success message
3. if the file is empty for any reason we must set * before the file extension like *.xlsx
4. delete the files after success import or download from the ftp 
5. the image must not be tomprary or stored on cloud or google drive or need to access
6. galaxus will try to access the image 5 time if he faild the image will not displayed
7. in the updating we should replace the existing files with the new files without renaming them or keep old files
8. also we will use filezila client to modify and handle the entire integration process

Important remarks for editing files on the productive FTP
General requirements for the data structure

No change of file name and no blanks or special characters are allowed

No column fixations

No line breaks in the header

No changes to the file structure (such as column sorting, etc.)

No additional tabs to be inserted

Uniform product identification by ProviderKey (= item number) in the first column consistent throughout the feed 

Remove EoL articles (= End of Life) from the price and stock data list (see "Remove/Delete")

Handling of CSV files
If the data feeds are prepared as CSV files ("Comma-Separated Values"), there are some points to consider when updating. Especially if the CSV files are not edited with a conventional text editor, but with Microsoft Excel.

Numeric values: Longer numeric sequences such as ProviderKeys or GTINs are sometimes automatically converted by Excel into a "scientific format" (e.g. 1234567890 becomes 1.24E+08). If a number sequence also contains leading zeros, these are also removed (e.g. 00505 becomes 505). Before uploading the revised data feeds to the FTP server, you must therefore check that the numerical values (especially the ProviderKeys and GTINs) are correctly stored in the CSV file.

UTF-8 encoding: After editing the CSV file, care should be taken to confirm UTF-8 encoding so that the file is not corrupted when saved and that no special characters are displayed that are not wanted.

Formatting: Any formatting done in Excel (e.g. colouring cells) is lost as soon as the CSV file is closed. Also, a CSV file cannot be saved with several worksheets (only the worksheet that is active when saving is retained). In general, the file structure should not be changed when revising the data feeds.

Newly uploaded files have always to replace the old files while being named exactly the same as the previous files. 

