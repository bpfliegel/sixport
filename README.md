Sixport is the C# port of the hexter DSSI software synthesizer plugin created by Sean Bolton and others. hexter is an open source emulation of the legendary Yamaha DX7 synthesizer. Changes done: OOP structure, algorithm specific rendering, LADSPA removal, speed improvements.

Additional notes
- Original hexter could be found here: http://dssi.sourceforge.net/hexter.html

Sixport differences are the following:
- Port to C#
- Removed the following:
- LADSPA interface functions
- locking/mutex mechanisms
- overlay buffer
Added the following:
- OOP structure
- DX7 algorithm specific calculation inlining for speed improvement
- Rendering speed improvements (around 3x faster than original raw port)
Silverlight 4 compatible code (currently introduced to Pluto: http://www.grape.hu/Pluto/Pluto2.html

Still to do:
- Clean up
- Parallel rendering
- Additional optimization

Currently renders 160 seconds of audio in 3.4 seconds (without parallel rendering of synth instances).
