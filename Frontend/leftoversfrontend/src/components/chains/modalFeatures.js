import styled, { css } from 'styled-components';
const sharedStyles = css`
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 1rem auto;
`;

const ModalHeader = styled.div`
  height: 5rem;
  border-bottom: 1px solid black;
  width: 90%;
  ${sharedStyles}
`;
const ModalBody = styled.div`
  height: 10rem;
  border-bottom: 1px solid black;
  width: 90%;
  ${sharedStyles}
`;

const ModalButtonsWrapper = styled.div`
  display: flex;
  flex-direction: row;
`;

const ModalButton = styled.button`
    height: 2rem;
    display: inline-block;
    background: cornflowerblue;
    border: none;
    color: black;
    font-weight: bold;
    width: 10rem;
    border-radius: 0.3rem;
    ${sharedStyles}
    
    &:hover {
      background: grey;
    }
`;