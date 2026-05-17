module instruction_memory(
    input wire [31:0] address,
    output wire [31:0] instruction
);
    reg [31:0] instruction_memory [0:1023];
    initial begin
       $readmemh("C:/Users/Emman/riscv_32i/instructions.hex", instruction_memory);
    end
    
    assign instruction = instruction_memory[address[11:2]];
endmodule