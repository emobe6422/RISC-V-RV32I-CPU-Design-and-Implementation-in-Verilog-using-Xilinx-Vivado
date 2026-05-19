`timescale 1ns / 1ps

module ALU(
    input wire [31:0] read_data_1,
    input wire [31:0] data_2_mux,
    input wire [3:0] ALU_control,
    input wire [2:0] funct3,
    output reg branch_flag,
    output reg [31:0] sum
    );
    
    always @(*) begin
        sum = 32'b0;
        branch_flag = 1'b0;
        case (ALU_control) 
            4'b0000 : sum = read_data_1 + data_2_mux; //add, addi
            4'b0001 : sum = read_data_1 ^ data_2_mux; //xor + xori
            4'b0010 : sum = read_data_1 | data_2_mux; //or + ori
            4'b0011 : sum = read_data_1 & data_2_mux; //and + andi
            4'b0100 : sum = read_data_1 << data_2_mux[4:0]; //sll + slli
            4'b0101 : sum = read_data_1 >> data_2_mux[4:0]; //srl + srli
            4'b0110 : sum = $signed(read_data_1) >>> data_2_mux[4:0]; //sra + srai
            4'b0111 : sum = read_data_1 - data_2_mux; //sub
            4'b1000 : begin //branch
                case (funct3) 
                    3'b000 : branch_flag = (read_data_1 == data_2_mux)? 1 : 0; //beq
                    3'b001 : branch_flag = (read_data_1 != data_2_mux)? 1 : 0; //bne
                    3'b100 :  branch_flag = ($signed(read_data_1) < $signed(data_2_mux))? 1 : 0; //blt
                    3'b101 : branch_flag = ($signed(read_data_1) >= $signed(data_2_mux))? 1 : 0; //bge
                    3'b110 : branch_flag = (read_data_1 < data_2_mux)? 1 : 0; //bltu
                    3'b111 : branch_flag = (read_data_1 >= data_2_mux)? 1 : 0; //bgeu
                endcase
            end

            4'b1001 : begin //sw, sh, sb
                sum = read_data_1 + data_2_mux;
            end
            4'b1010 : begin //lb, lh, lw, lbu, lhu
                //first implement lw
                sum = read_data_1 + data_2_mux;
            end
            4'b1011 : sum = ($signed(read_data_1) < $signed(data_2_mux)) ? 32'b1 : 32'b0; // slt/slti
            4'b1100 : sum = (read_data_1 < data_2_mux) ? 32'b1 : 32'b0; // sltu/sltiu
            4'b1101 : sum = data_2_mux; //lui
         
        
            4'b1110 : begin
        
            end
        
            4'b1111 : begin
        
            end
        endcase
    end
endmodule
