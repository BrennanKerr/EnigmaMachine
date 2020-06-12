# EnigmaMachine

# What is the Enigma Machine?
The enigma machine is an encryption device widely known for its use by the German forces during World War 2.

# How does it work?
Once a key is pressed, the letter goes through the plugboard (if set up), then through the 3 rotors (from Rotor 3 to Rotor 1),
to the reflector, then back through the rotors and plugboard. This allows for over 150,000,000,000,000 different combinations, 
which is why it was such a powerful tool during the war.

# How the Application Works?
During initialization, the program obtains the rotor and reflector information from the corresponding files in the Debug/files folder.
From there, the program loads and allows the user to set up the initial settings for the machine.

# How to Use the Program
The initial settings for the program are Reflector B, Rotors I, II, and III with offsets of AAA, and no plugboard combinations.
Once your desired settings are selected, simply press a key and the corresponding letter on the lamp board will light up. A textbox and the Debug/files/output.txt will store the encrypted string.

# Test Run
If application works properly, the initial settings listed above with AAAAA entered should result in BDZGO.

# Other
If there are any issues with the application, please leave a request and it will be looked into.
If any code from the program is used, please give me credit.

# Resources
Rotor and Reflector settings were obtain from Wikipedia:
https://en.wikipedia.org/wiki/Enigma_machine