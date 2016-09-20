clearvars;
close all;

grav = 0; %gravity in z direction
%define robot paramaeters
Link0_radius=40;
r0=0; g0=1; b0=0;
Link1_length = 200;
Link1_width = 15;
r1=1; g1=0; b1=0;
Link2_length = 100;
Link2_width = 10;
r2=0; g2=0; b2=1;
sim('second');