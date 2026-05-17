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
            4'b0000 : begin //add, addi
                sum = read_data_1 + data_2_mux;
            end
        
            4'b0001 : begin //xor + xori
                sum = read_data_1 ^ data_2_mux;
            end
        
            4'b0010 : begin //or + ori
                sum = read_data_1 | data_2_mux;
            end

            4'b0011 : begin //and + andi
                sum = read_data_1 & data_2_mux;
            end
        
        
            4'b0100 : begin //sll + slli
                sum = read_data_1 << data_2_mux[4:0];
            end
        
        
            4'b0101 : begin //srl + srli
                sum = read_data_1 >> data_2_mux[4:0];
            end
        
        
            4'b0110 : begin //sra + srai
                sum = $signed(read_data_1) >>> data_2_mux[4:0];
            end
        
        
            4'b0111 : begin //sub
                sum = read_data_1 - data_2_mux;
            end
        
        
            4'b1000 : begin //branch
                case (funct3) 
                    3'b000 : begin //beq
                        branch_flag = (read_data_1 == data_2_mux)? 1 : 0;
                    end
                    3'b001 : begin //bne
                        branch_flag = (read_data_1 != data_2_mux)? 1 : 0;
                    
                    end
                    3'b100 : begin //blt
                        branch_flag = ($signed(read_data_1) < $signed(data_2_mux))? 1 : 0;
                    
                    end
                    3'b101 : begin //bge
                         branch_flag = ($signed(read_data_1) >= $signed(data_2_mux))? 1 : 0;
                    
                    end
                    3'b110 : begin //bltu
                          branch_flag = (read_data_1 < data_2_mux)? 1 : 0;
                    
                    end
                    3'b111 : begin //bgeu
                          branch_flag = (read_data_1 >= data_2_mux)? 1 : 0;
                    end
                endcase
            end

            4'b1001 : begin //sw, sh, sb
                sum = read_data_1 + data_2_mux;
            end
        
        
            4'b1010 : begin //lb, lh, lw, lbu, lhu
                //first implement lw
                sum = read_data_1 + data_2_mux;
            end
        
           4'b1011: begin // slt
                sum = ($signed(read_data_1) < $signed(data_2_mux)) ? 32'b1 : 32'b0;
            end
            
            4'b1100: begin // sltu
                sum = (read_data_1 < data_2_mux) ? 32'b1 : 32'b0;
            end
        
            4'b1101 : begin
        
            end
        
            4'b1110 : begin
        
            end
        
            4'b1111 : begin
        
            end
        endcase
    end
endmodule
