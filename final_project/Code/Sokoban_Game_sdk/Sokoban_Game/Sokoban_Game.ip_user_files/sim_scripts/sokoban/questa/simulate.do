onbreak {quit -f}
onerror {quit -f}

vsim -t 1ps -lib xil_defaultlib sokoban_opt

do {wave.do}

view wave
view structure
view signals

do {sokoban.udo}

run -all

quit -force
