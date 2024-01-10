# This file is automatically generated.
# It contains project source information necessary for synthesis and implementation.

# Block Designs: bd/hw2/hw2.bd
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2 || ORIG_REF_NAME==hw2} -quiet] -quiet

# IP: bd/hw2/ip/hw2_processing_system7_0_0/hw2_processing_system7_0_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_processing_system7_0_0 || ORIG_REF_NAME==hw2_processing_system7_0_0} -quiet] -quiet

# IP: bd/hw2/ip/hw2_ps7_0_axi_periph_0/hw2_ps7_0_axi_periph_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_ps7_0_axi_periph_0 || ORIG_REF_NAME==hw2_ps7_0_axi_periph_0} -quiet] -quiet

# IP: bd/hw2/ip/hw2_rst_ps7_0_50M_0/hw2_rst_ps7_0_50M_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_rst_ps7_0_50M_0 || ORIG_REF_NAME==hw2_rst_ps7_0_50M_0} -quiet] -quiet

# IP: bd/hw2/ip/hw2_axi_gpio_0_1/hw2_axi_gpio_0_1.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_0_1 || ORIG_REF_NAME==hw2_axi_gpio_0_1} -quiet] -quiet

# IP: bd/hw2/ip/hw2_axi_gpio_1_0/hw2_axi_gpio_1_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_1_0 || ORIG_REF_NAME==hw2_axi_gpio_1_0} -quiet] -quiet

# IP: bd/hw2/ip/hw2_xbar_0/hw2_xbar_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_xbar_0 || ORIG_REF_NAME==hw2_xbar_0} -quiet] -quiet

# IP: bd/hw2/ip/hw2_auto_pc_0/hw2_auto_pc_0.xci
set_property DONT_TOUCH TRUE [get_cells -hier -filter {REF_NAME==hw2_auto_pc_0 || ORIG_REF_NAME==hw2_auto_pc_0} -quiet] -quiet

# XDC: bd/hw2/ip/hw2_processing_system7_0_0/hw2_processing_system7_0_0.xdc
set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_processing_system7_0_0 || ORIG_REF_NAME==hw2_processing_system7_0_0} -quiet] {/inst } ]/inst ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_rst_ps7_0_50M_0/hw2_rst_ps7_0_50M_0_board.xdc
set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_rst_ps7_0_50M_0 || ORIG_REF_NAME==hw2_rst_ps7_0_50M_0} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_rst_ps7_0_50M_0/hw2_rst_ps7_0_50M_0.xdc
#dup# set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_rst_ps7_0_50M_0 || ORIG_REF_NAME==hw2_rst_ps7_0_50M_0} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_axi_gpio_0_1/hw2_axi_gpio_0_1_board.xdc
set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_0_1 || ORIG_REF_NAME==hw2_axi_gpio_0_1} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_axi_gpio_0_1/hw2_axi_gpio_0_1_ooc.xdc

# XDC: bd/hw2/ip/hw2_axi_gpio_0_1/hw2_axi_gpio_0_1.xdc
#dup# set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_0_1 || ORIG_REF_NAME==hw2_axi_gpio_0_1} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_axi_gpio_1_0/hw2_axi_gpio_1_0_board.xdc
set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_1_0 || ORIG_REF_NAME==hw2_axi_gpio_1_0} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_axi_gpio_1_0/hw2_axi_gpio_1_0_ooc.xdc

# XDC: bd/hw2/ip/hw2_axi_gpio_1_0/hw2_axi_gpio_1_0.xdc
#dup# set_property DONT_TOUCH TRUE [get_cells [split [join [get_cells -hier -filter {REF_NAME==hw2_axi_gpio_1_0 || ORIG_REF_NAME==hw2_axi_gpio_1_0} -quiet] {/U0 } ]/U0 ] -quiet] -quiet

# XDC: bd/hw2/ip/hw2_xbar_0/hw2_xbar_0_ooc.xdc

# XDC: bd/hw2/ip/hw2_auto_pc_0/hw2_auto_pc_0_ooc.xdc

# XDC: bd/hw2/hw2_ooc.xdc
