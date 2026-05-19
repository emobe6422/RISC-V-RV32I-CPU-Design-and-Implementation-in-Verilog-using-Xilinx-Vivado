`timescale 1ns / 1ps

module program_counter_register(
    input wire clk,
    input wire [31:0] next_PC,
    output reg [31:0] PC
);

    //REMOVE LATER
    initial begin
        PC = 32'b0;
    end

    always @(posedge clk) begin
        PC <= next_PC;
    end

endmodule